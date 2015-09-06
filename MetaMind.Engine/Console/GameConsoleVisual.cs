namespace MetaMind.Engine.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Components.Fonts;
    using Components.Graphics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Primtives2D;
    using Settings.Loaders;

    public class GameConsoleVisual :
        GameModuleVisual<GameConsole, GameConsoleSettings, GameConsoleLogic, GameConsoleVisual>,
        IParameterLoader<GraphicsSettings>
    {
        #region Dependency

        private readonly SpriteBatch spriteBatch;

        private readonly IStringDrawer stringDrawer;

        private ScrollController Scroll { get; set; } = new ScrollController();

        #endregion

        #region Positional Settings

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

        private float oneCharacterWidth;

        private int maxCharactersPerLine;

        #endregion

        #region Positional States

        private Vector2 currentPosition;

        private Vector2 firstCommandPositionOffset = Vector2.Zero;

        private Vector2 FirstCommandPosition => new Vector2(this.TextBounds.X, this.TextBounds.Y) + this.firstCommandPositionOffset + this.Scroll.ScrollOffset;

        private Rectangle BackgroundBounds => new Rectangle(
            (int)this.currentPosition.X,
            (int)this.currentPosition.Y,
            this.viewportWidth - this.Settings.Margin * 2,
            this.Settings.Height);

        private Rectangle TextBounds => new Rectangle(
            this.BackgroundBounds.X + this.Settings.Padding,
            this.BackgroundBounds.Y + this.Settings.Padding,
            this.BackgroundBounds.Width - this.Settings.Padding * 2,
            this.BackgroundBounds.Height - this.Settings.Padding * 2);

        #endregion

        #region Positional Helpers

        private bool IsInsideBounds(Vector2 position)
        {
            return position.Y < this.openedPosition.Y + this.Settings.Height;
        }

        #endregion

        #region Positional Operations

        private void AmendCommandPosition(Vector2 position, float leading)
        {
            if (!this.IsInsideBounds(position))
            {
                this.firstCommandPositionOffset.Y -= leading;
            }
        }

        public void ResetCommandPosition()
        {
            this.firstCommandPositionOffset = Vector2.Zero;
        }

        #endregion

        #region Visual States

        private GameConsoleState currentState;

        public bool IsOpened => this.currentState == GameConsoleState.Opened;

        #endregion

        #region Constructors and Finalizer

        public GameConsoleVisual(GameConsole module, GameEngine engine, SpriteBatch spriteBatch, IStringDrawer stringDrawer)
            : base(module, engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            if (spriteBatch == null)
            {
                throw new ArgumentNullException(nameof(spriteBatch));
            }

            if (stringDrawer == null)
            {
                throw new ArgumentNullException(nameof(stringDrawer));
            }

            this.spriteBatch  = spriteBatch;
            this.stringDrawer = stringDrawer;
        }

        #endregion

        #region Initialization

        public override void Initialize()
        {
            this.LoadParameter(this.Engine.Graphics.Settings);

            this.InitializeState();
            this.InitializePosition();

            base.Initialize();
        }

        private void InitializeState()
        {
            this.currentState = GameConsoleState.Closed;
        }

        private void InitializePosition()
        {
            this.currentPosition = this.closedPosition = new Vector2(this.Settings.Margin, -this.Settings.Height);
            this.openedPosition = new Vector2(this.Settings.Margin, 0);

            this.oneCharacterWidth = this.Settings.Font.MeasureMonospacedString("x", 1f).X;
            this.maxCharactersPerLine = (int)((this.BackgroundBounds.Width - this.Settings.Padding * 2) / this.oneCharacterWidth);
        }

        public void LoadParameter(GraphicsSettings parameter)
        {
            this.viewportWidth = parameter.Width;

            this.Scroll.LoadParameter(parameter);
        }

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

        public override void Draw(GameTime time)
        {
            if (!this.Module.Enabled)
            {
                return;
            }

            if (this.currentState == GameConsoleState.Closed)
            {
                return;
            }

            this.spriteBatch.Begin();

            this.DrawBackground();

            var promptPosition = this.DrawPastOutput();
            var bufferPosition = this.DrawPrompt(promptPosition);
            var cursorPosition = this.DrawBuffer(bufferPosition); 
                                 this.DrawCursor(cursorPosition, time);

            this.spriteBatch.End();
        }

        private void DrawBackground()
        {
            Primitives2D.FillRectangle(this.spriteBatch, this.BackgroundBounds, this.Settings.BackgroundColor);
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
                    this.currentPosition.X + this.BackgroundBounds.Width - this.Settings.RoundedCorner.Width,
                    this.currentPosition.Y + this.Settings.Height),
                null, this.Settings.BackgroundColor, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 1);

            // Connecting bottom-rectangle
            Primitives2D.DrawRectangle(
                this.spriteBatch,
                new Rectangle(
                    this.BackgroundBounds.X + this.Settings.RoundedCorner.Width,
                    this.BackgroundBounds.Y + this.Settings.Height,
                    this.BackgroundBounds.Width - this.Settings.RoundedCorner.Width * 2,
                    this.Settings.RoundedCorner.Height),
                this.Settings.BackgroundColor);
        }

        private Vector2 DrawBuffer(Vector2 position)
        {
            return this.DrawCommand(this.Settings.Font, this.Logic.Input.ToString(), position, this.Settings.BufferColor);
        }

        private void DrawCursor(Vector2 position, GameTime time)
        {
            if (!this.IsInsideBounds(position))
            {
                return;
            }

            var split = StringUtils.BreakStringByCharacterToEnumerable(
                    this.Logic.Input.ToString(),
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
        private Vector2 DrawCommands(Font font, IEnumerable<CommandLine> lines, Vector2 position)
        {
            var firstColumn = position.X;

            foreach (var line in lines.ToArray())
            {
                // Add prompt before a line of command
                if (line.Type == CommandType.Input)
                {
                    // Draw executed command prompt 
                    position = this.DrawPrompt(position);
                }

                var color = line.Type == CommandType.Input
                                ? this.Settings.PastBufferColor
                                : line.Type == CommandType.Output
                                      ? this.Settings.PastOutputColor
                                      : line.Type == CommandType.Debug
                                            ? this.Settings.PastDebugColor
                                            : this.Settings.PastErrorColor;

                var nextCommandPosition = this.DrawCommand(
                    font,
                    line.ToString(), 
                    position,
                    color);

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
        /// <returns></returns>
        private Vector2 DrawCommand(Font font, string command, Vector2 position, Color color)
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

                if (!this.Scroll.IsEnabled)
                {
                    this.AmendCommandPosition(position + new Vector2(0, leading), leading);
                }
            }

            return position;
        }

        private Vector2 DrawPastOutput()
        {
            return this.DrawCommands(this.Settings.Font, this.Logic.Output, this.FirstCommandPosition);
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

        public override void Update(GameTime time)
        {
            switch (this.currentState)
            {
                case GameConsoleState.Opening:
                    {
                        this.UpdateState(this.openedPosition, GameConsoleState.Opened);
                    }

                    break;

                case GameConsoleState.Closing:
                    {
                        this.UpdateState(this.closedPosition, GameConsoleState.Closed);
                    }

                    break;
            }
        }

        private void UpdateState(Vector2 targetPosition, GameConsoleState targetState)
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
            if (this.currentState == GameConsoleState.Opening || 
                this.currentState == GameConsoleState.Opened)
            {
                return;
            }

            this.stateTransitionMoment = DateTime.Now;
            this.currentState = GameConsoleState.Opening;
        }

        public void Close()
        {
            if (this.currentState == GameConsoleState.Closing || 
                this.currentState == GameConsoleState.Closed)
            {
                return;
            }

            this.stateTransitionMoment = DateTime.Now;
            this.currentState = GameConsoleState.Closing;
        }

        public void PageUp()
        {
            this.Scroll.PageUp();
        }

        public void PageDown()
        {
            this.Scroll.PageDown();
        }

        public void PageReset()
        {
            this.Scroll.PageReset();
        }

        #endregion
    }
}