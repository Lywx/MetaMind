namespace MetaMind.Engine.Guis.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MetaMind.Engine.Components.Fonts;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class Renderer : IDisposable
    {
        private enum State
        {
            Opened,
            Opening,
            Closed,
            Closing
        }

        public bool IsOpen
        {
            get
            {
                return this.currentState == State.Opened;
            }
        }

        private readonly SpriteBatch spriteBatch;

        private readonly IStringDrawer stringDrawer;

        private readonly InputProcessor inputProcessor;

        private readonly Texture2D pixel;

        private readonly int width;

        private State currentState;

        private Vector2 openedPosition, closedPosition, position;

        private DateTime stateChangeTime;

        private Vector2 firstCommandPositionOffset;

        private Vector2 FirstCommandPosition
        {
            get
            {
                return new Vector2(this.InnerBounds.X, this.InnerBounds.Y) + this.firstCommandPositionOffset;
            }
        }

        private Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                    (int)this.position.X,
                    (int)this.position.Y,
                    this.width - (GameConsoleOptions.Options.Margin * 2),
                    GameConsoleOptions.Options.Height);
            }
        }

        private Rectangle InnerBounds
        {
            get
            {
                return new Rectangle(
                    this.Bounds.X + GameConsoleOptions.Options.Padding,
                    this.Bounds.Y + GameConsoleOptions.Options.Padding,
                    this.Bounds.Width - GameConsoleOptions.Options.Padding,
                    this.Bounds.Height);
            }
        }

        private readonly float oneCharacterWidth;

        private readonly int maxCharactersPerLine;

        public Renderer(Game game, SpriteBatch spriteBatch, IStringDrawer stringDrawer, InputProcessor inputProcessor)
        {
            this.currentState = State.Closed;
            this.width = game.GraphicsDevice.Viewport.Width;
            this.position = this.closedPosition = new Vector2(GameConsoleOptions.Options.Margin, -GameConsoleOptions.Options.Height);
            this.openedPosition = new Vector2(GameConsoleOptions.Options.Margin, 0);
            this.spriteBatch = spriteBatch;
            this.stringDrawer = stringDrawer;
            this.inputProcessor = inputProcessor;
            this.pixel = new Texture2D(game.GraphicsDevice, 1, 1);
            this.pixel.SetData(new[] { Color.White });
            this.firstCommandPositionOffset = Vector2.Zero;
            this.oneCharacterWidth = GameConsoleOptions.Options.Font.MeasureString("x").X;
            this.maxCharactersPerLine = (int)((this.Bounds.Width - GameConsoleOptions.Options.Padding * 2) / this.oneCharacterWidth);
        }

        public void Update(GameTime gameTime)
        {
            if (this.currentState == State.Opening)
            {
                this.position.Y = MathHelper.SmoothStep(
                    this.position.Y,
                    this.openedPosition.Y,
                    (float)((DateTime.Now - this.stateChangeTime).TotalSeconds / GameConsoleOptions.Options.AnimationSpeed));

                if (Math.Abs(this.position.Y - this.openedPosition.Y) < 0.1f)
                {
                    this.currentState = State.Opened;
                }
            }

            if (this.currentState == State.Closing)
            {
                this.position.Y = MathHelper.SmoothStep(
                    this.position.Y,
                    this.closedPosition.Y,
                    (float)((DateTime.Now - this.stateChangeTime).TotalSeconds / GameConsoleOptions.Options.AnimationSpeed));

                if (this.position.Y == this.closedPosition.Y)
                {
                    this.currentState = State.Closed;
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            // Do not draw if the console is closed
            if (this.currentState == State.Closed)
            {
                return;
            }

            this.spriteBatch.Draw(this.pixel, this.Bounds, GameConsoleOptions.Options.BackgroundColor);

            // DrawRoundedEdges();
            var nextCommandPosition = this.DrawCommands(this.inputProcessor.Out, this.FirstCommandPosition);
            nextCommandPosition = this.DrawPrompt(nextCommandPosition);
            var bufferPosition = this.DrawCommand(
                this.inputProcessor.Buffer.ToString(),
                nextCommandPosition,
                GameConsoleOptions.Options.BufferColor); // Draw the buffer
            this.DrawCursor(bufferPosition, gameTime);
        }

        private void DrawRoundedEdges()
        {
            // Bottom-left edge
            this.spriteBatch.Draw(
                GameConsoleOptions.Options.RoundedCorner,
                new Vector2(this.position.X, this.position.Y + GameConsoleOptions.Options.Height),
                null,
                GameConsoleOptions.Options.BackgroundColor,
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                1);

            // Bottom-right edge
            this.spriteBatch.Draw(
                GameConsoleOptions.Options.RoundedCorner,
                new Vector2(
                    this.position.X + this.Bounds.Width - GameConsoleOptions.Options.RoundedCorner.Width,
                    this.position.Y + GameConsoleOptions.Options.Height),
                null,
                GameConsoleOptions.Options.BackgroundColor,
                0,
                Vector2.Zero,
                1,
                SpriteEffects.FlipHorizontally,
                1);

            // Connecting bottom-rectangle
            this.spriteBatch.Draw(
                this.pixel,
                new Rectangle(
                    this.Bounds.X + GameConsoleOptions.Options.RoundedCorner.Width,
                    this.Bounds.Y + GameConsoleOptions.Options.Height,
                    this.Bounds.Width - GameConsoleOptions.Options.RoundedCorner.Width * 2,
                    GameConsoleOptions.Options.RoundedCorner.Height),
                GameConsoleOptions.Options.BackgroundColor);
        }

        private void DrawCursor(Vector2 pos, GameTime gameTime)
        {
            if (!this.IsInBounds(pos.Y))
            {
                return;
            }

            var split = SplitCommand(this.inputProcessor.Buffer.ToString(), this.maxCharactersPerLine).Last();

            pos.X += GameConsoleOptions.Options.Font.MeasureMonospacedString(split, 1f).X;
            pos.Y -= GameConsoleOptions.Options.Font.GetSprite().LineSpacing;

            this.stringDrawer.DrawMonospacedString(
                GameConsoleOptions.Options.Font,
                (int)(gameTime.TotalGameTime.TotalSeconds / GameConsoleOptions.Options.CursorBlinkSpeed) % 2 == 0
                    ? GameConsoleOptions.Options.Cursor.ToString()
                    : "",
                pos,
                GameConsoleOptions.Options.CursorColor,
                1f);
        }

        /// <summary>
        ///     Draws the specified command and returns the position of the next command to be drawn
        /// </summary>
        /// <param name="command"></param>
        /// <param name="pos"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private Vector2 DrawCommand(string command, Vector2 pos, Color color)
        {
            var splitLines = command.Length > this.maxCharactersPerLine
                                 ? SplitCommand(command, this.maxCharactersPerLine)
                                 : new[] { command };
            foreach (var line in splitLines)
            {
                if (this.IsInBounds(pos.Y))
                {
                    this.stringDrawer.DrawMonospacedString(GameConsoleOptions.Options.Font, line, pos, color, 1f);
                }

                this.ValidateFirstCommandPosition(pos.Y + GameConsoleOptions.Options.Font.GetSprite().LineSpacing);
                pos.Y += GameConsoleOptions.Options.Font.GetSprite().LineSpacing;
            }
            return pos;
        }

        private static IEnumerable<string> SplitCommand(string command, int max)
        {
            var lines = new List<string>();
            while (command.Length > max)
            {
                var splitCommand = command.Substring(0, max);
                lines.Add(splitCommand);
                command = command.Substring(max, command.Length - max);
            }
            lines.Add(command);
            return lines;
        }

        /// <summary>
        ///     Draws the specified collection of commands and returns the position of the next command to be drawn
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private Vector2 DrawCommands(IEnumerable<OutputLine> lines, Vector2 pos)
        {
            var originalX = pos.X;
            foreach (var command in lines)
            {
                if (command.Type == OutputLineType.Command)
                {
                    pos = this.DrawPrompt(pos);
                }
                //position.Y = DrawCommand(command.ToString(), position, GameConsoleOptions.Options.FontColor).Y;
                pos.Y =
                    this.DrawCommand(
                        command.ToString(),
                        pos,
                        command.Type == OutputLineType.Command
                            ? GameConsoleOptions.Options.PastCommandColor
                            : GameConsoleOptions.Options.PastCommandOutputColor).Y;
                pos.X = originalX;
            }
            return pos;
        }

        /// <summary>
        ///     Draws the prompt at the specified position and returns the position of the text that will be drawn next to it
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private Vector2 DrawPrompt(Vector2 pos)
        {
            this.stringDrawer.DrawMonospacedString(
                GameConsoleOptions.Options.Font,
                GameConsoleOptions.Options.Prompt,
                pos,
                GameConsoleOptions.Options.PromptColor, 
                1f);

            pos.X += this.oneCharacterWidth * GameConsoleOptions.Options.Prompt.Length + this.oneCharacterWidth;
            return pos;
        }

        public void Open()
        {
            if (this.currentState == State.Opening || this.currentState == State.Opened)
            {
                return;
            }
            this.stateChangeTime = DateTime.Now;
            this.currentState = State.Opening;
        }

        public void Close()
        {
            if (this.currentState == State.Closing || this.currentState == State.Closed)
            {
                return;
            }
            this.stateChangeTime = DateTime.Now;
            this.currentState = State.Closing;
        }

        private void ValidateFirstCommandPosition(float nextCommandY)
        {
            if (!this.IsInBounds(nextCommandY))
            {
                this.firstCommandPositionOffset.Y -= GameConsoleOptions.Options.Font.GetSprite().LineSpacing;
            }
        }

        private bool IsInBounds(float yPosition)
        {
            return yPosition < this.openedPosition.Y + GameConsoleOptions.Options.Height;
        }

        public void Dispose()
        {
            this.pixel.Dispose();
        }
    }
}