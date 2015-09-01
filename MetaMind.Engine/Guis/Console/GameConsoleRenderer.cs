﻿namespace MetaMind.Engine.Guis.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Components.Fonts;
    using Components.Graphics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Settings.Loaders;

    internal class GameConsoleRenderer : IParameterLoader<GraphicsSettings>, IDisposable
    {
        #region States

        private enum State
        {
            Opened,

            Opening,

            Closed,

            Closing
        }

        private State currentState;

        public bool IsOpened => this.currentState == State.Opened;

        #endregion

        private readonly SpriteBatch spriteBatch;

        private readonly IStringDrawer stringDrawer;

        private readonly GameConsoleProcessor processor;

        private Texture2D pixel;

        #region Positional States

        private int viewportWidth;

        /// <summary>
        /// Position of fully displayed console in the screen.
        /// </summary>
        private Vector2 openedPosition;

        /// <summary>
        /// Position of fully closed console in the screen. This is outside the 
        /// viewport.
        /// </summary>
        private Vector2 closedPosition;

        private Vector2 currentPosition;

        private float oneCharacterWidth;

        private int maxCharactersPerLine;

        private Vector2 firstCommandPositionOffset = Vector2.Zero;

        private Vector2 FirstCommandPosition => new Vector2(this.InnerBounds.X, this.InnerBounds.Y) + this.firstCommandPositionOffset;

        private Rectangle Bounds => new Rectangle(
            (int)this.currentPosition.X,
            (int)this.currentPosition.Y,
            this.viewportWidth - this.Settings.Margin * 2,
            this.Settings.Height);

        private Rectangle InnerBounds => new Rectangle(
            this.Bounds.X + this.Settings.Padding,
            this.Bounds.Y + this.Settings.Padding,
            this.Bounds.Width - this.Settings.Padding * 2,
            this.Bounds.Height - this.Settings.Padding * 2);

        private bool IsInsideBounds(Vector2 position)
        {
            return position.Y < this.openedPosition.Y + this.Settings.Height;
        }

        private void AmendCommandPosition(Vector2 nextCommandPosition, float leading)
        {
            if (!this.IsInsideBounds(nextCommandPosition))
            {
                this.firstCommandPositionOffset.Y -= leading;
            }
        }

        public void ResetCommandPosition()
        {
            this.firstCommandPositionOffset = Vector2.Zero;
        }

        #endregion

        #region Constructors and Finalizer

        /// <remarks>
        /// Renderer has to be constructed after the font is loaded.
        /// </remarks>
        public GameConsoleRenderer(GameEngine engine, GameConsoleProcessor processor, SpriteBatch spriteBatch, IStringDrawer stringDrawer)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            if (processor == null)
            {
                throw new ArgumentNullException(nameof(processor));
            }

            if (spriteBatch == null)
            {
                throw new ArgumentNullException(nameof(spriteBatch));
            }

            if (stringDrawer == null)
            {
                throw new ArgumentNullException(nameof(stringDrawer));
            }

            this.processor = processor;

            this.spriteBatch  = spriteBatch;
            this.stringDrawer = stringDrawer;

            this.LoadParameter(engine.Graphics.Settings);
            this.InitializeState();
            this.InitializePosition();
            this.InitializePixel(engine.GraphicsDevice);
        }

        ~GameConsoleRenderer()
        {
            this.Dispose();
        }

        #endregion

        #region Initialization

        public void LoadParameter(GraphicsSettings parameter)
        {
            this.viewportWidth = parameter.Width;
        }

        private void InitializeState()
        {
            this.currentState = State.Closed;
        }

        private void InitializePixel(GraphicsDevice graphicsDevice)
        {
            this.pixel = new Texture2D(graphicsDevice, 1, 1);
            this.pixel.SetData(
                new[]
                {
                    Color.White
                });
        }

        private void InitializePosition()
        {
            this.currentPosition = this.closedPosition =
                                   new Vector2(
                                       this.Settings.Margin,
                                       -this.Settings.Height);
            this.openedPosition = new Vector2(this.Settings.Margin, 0);

            this.oneCharacterWidth =
                this.Settings.Font.MeasureMonospacedString("x", 1f).X;
            this.maxCharactersPerLine =
                (int)
                ((this.Bounds.Width - this.Settings.Padding * 2)
                 / this.oneCharacterWidth);
        }


        #endregion

        #region Settings

        private GameConsoleSettings Settings => GameConsoleSettings.Settings;

        #endregion

        #region Animation

        private DateTime stateTransitionMoment;

        private float StateTransitionAmount(DateTime moment)
        {
            var transitionDuration = moment - this.stateTransitionMoment;
            return (float)(transitionDuration.TotalSeconds / this.Settings.AnimationSpeed);
        }

        #endregion

        #region Draw

        public void Draw(GameTime time)
        {
            if (this.currentState == State.Closed)
            {
                return;
            }

            this.DrawRectangle();

            var promptPosition = this.DrawOutput();
            var bufferPosition = this.DrawPrompt(promptPosition);
            var cursorPosition = this.DrawBuffer(bufferPosition); 
                                 this.DrawCursor(cursorPosition, time);
        }

        private void DrawRectangle()
        {
            this.spriteBatch.Draw(
                this.pixel,
                this.Bounds,
                this.Settings.BackgroundColor);
        }

        private void DrawRoundedEdges()
        {
            // Bottom-left edge
            this.spriteBatch.Draw(
                this.Settings.RoundedCorner,
                new Vector2(
                    this.currentPosition.X,
                    this.currentPosition.Y + this.Settings.Height),
                null, this.Settings.BackgroundColor, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

            // Bottom-right edge
            this.spriteBatch.Draw(
                this.Settings.RoundedCorner,
                new Vector2(
                    this.currentPosition.X + this.Bounds.Width - this.Settings.RoundedCorner.Width,
                    this.currentPosition.Y + this.Settings.Height),
                null, this.Settings.BackgroundColor, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 1);

            // Connecting bottom-rectangle
            this.spriteBatch.Draw(
                this.pixel,
                new Rectangle(
                    this.Bounds.X + this.Settings.RoundedCorner.Width,
                    this.Bounds.Y + this.Settings.Height,
                    this.Bounds.Width - this.Settings.RoundedCorner.Width * 2,
                    this.Settings.RoundedCorner.Height),
                this.Settings.BackgroundColor);
        }

        private Vector2 DrawBuffer(Vector2 bufferPosition)
        {
            return this.DrawCommand(this.Settings.Font, this.processor.CommandBuffer.ToString(), bufferPosition, this.Settings.BufferColor, this.Settings);
        }

        private void DrawCursor(Vector2 position, GameTime time)
        {
            if (!this.IsInsideBounds(position))
            {
                return;
            }

            var split = StringUtils.BreakStringByCharacterToEnumerable(
                    this.processor.CommandBuffer.ToString(),
                    this.maxCharactersPerLine).Last();

            var font = this.Settings.Font;

            position.X += font.MeasureMonospacedString(split, 1f).X;
            position.Y -= font.Sprite().LineSpacing;

            var cursor = (int)(time.TotalGameTime.TotalSeconds / this.Settings.CursorBlinkSpeed) % 2 == 0 ? this.Settings.Cursor.ToString() : "";

            this.stringDrawer.DrawMonospacedString(font, cursor, position, this.Settings.CursorColor, 1f);
        }

        /// <summary>
        ///     Draws the specified collection of commands and returns the 
        /// position of the next command to be drawn.
        /// </summary>
        /// <param name="font"></param>
        /// <param name="lines"></param>
        /// <param name="position">Position of the command to draw</param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private Vector2 DrawCommands(Font font, IEnumerable<OutputLine> lines, Vector2 position, GameConsoleSettings settings)
        {
            var firstColumn = position.X;

            foreach (var command in lines.ToArray())
            {
                // Add prompt before a line of command
                if (command.Type == OutputType.Buffer)
                {
                    // Draw executed command prompt 
                    position = this.DrawPrompt(position);
                }

                var color = command.Type == OutputType.Buffer
                                ? settings.PastBufferColor
                                : command.Type == OutputType.Output
                                      ? settings.PastOutputColor
                                      : command.Type == OutputType.Debug
                                            ? settings.PastDebugColor
                                            : settings.PastErrorColor;

                var nextCommandPosition = this.DrawCommand(
                    font,
                    command.ToString(), 
                    position,
                    color,
                    settings);

                position.Y = nextCommandPosition.Y;

                // Restore the horizontal position in each line
                position.X = firstColumn;
            }

            return position;
        }

        /// <summary>
        ///     Draws the specified command and returns the position of the next command to be drawn
        /// </summary>
        /// <param name="font"></param>
        /// <param name="command"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private Vector2 DrawCommand(Font font, string command, Vector2 position, Color color, GameConsoleSettings settings)
        {
            var commandLines = command.Length > this.maxCharactersPerLine
                                          ? StringUtils.BreakStringByCharacterToEnumerable(command, this.maxCharactersPerLine) : new[] { command };

            foreach (var line in commandLines)
            {
                if (this.IsInsideBounds(position))
                {
                    this.stringDrawer.DrawMonospacedString(font, line, position, color, 1f);
                }

                var leading = font.Mono().AsciiSize(1f).Y; 
                position.Y += leading;

                this.AmendCommandPosition(position + new Vector2(0, leading), leading);
            }

            return position;
        }

        private Vector2 DrawOutput()
        {
            return this.DrawCommands(this.Settings.Font, this.processor.CommandOutput, this.FirstCommandPosition, this.Settings);
        }

        /// <summary>
        ///     Draws the prompt at the specified position and returns the position of the text that will be drawn next to it
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private Vector2 DrawPrompt(Vector2 position)
        {
            this.stringDrawer.DrawMonospacedString(this.Settings.Font, this.Settings.Prompt, position, this.Settings.PromptColor, 1f);

            // Add space after prompt (1 stands for the empty space)
            position.X += this.oneCharacterWidth * (1 + this.Settings.Prompt.Length);
            return position;
        }

        #endregion

        #region Update

        public void Update(GameTime time)
        {
            switch (this.currentState)
            {
                case State.Opening:
                    this.UpdateState(this.openedPosition, State.Opened);
                    break;

                case State.Closing:
                    this.UpdateState(this.closedPosition, State.Closed);
                    break;
            }
        }

        private void UpdateState(Vector2 targetPosition, State targetState)
        {
            this.currentPosition.Y = MathHelper.SmoothStep(
                    this.currentPosition.Y,
                    targetPosition.Y,
                    this.StateTransitionAmount(DateTime.Now));

            if (Math.Abs(this.currentPosition.Y - targetPosition.Y) < 0.1f)
            {
                this.currentState = targetState;
            }
        }

        #endregion

        #region Operations

        public void Open()
        {
            if (this.currentState == State.Opening || 
                this.currentState == State.Opened)
            {
                return;
            }

            this.stateTransitionMoment = DateTime.Now;
            this.currentState = State.Opening;
        }

        public void Close()
        {
            if (this.currentState == State.Closing || 
                this.currentState == State.Closed)
            {
                return;
            }

            this.stateTransitionMoment = DateTime.Now;
            this.currentState = State.Closing;
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            this.pixel?.Dispose();
            this.pixel = null;
        }

        #endregion
    }
}