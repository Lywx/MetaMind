namespace MetaMind.Engine.Guis.Console
{
    using System;
    using Commands;
    using Components.Fonts;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal partial class GameConsoleComponent : DrawableGameComponent
    {
        private readonly GameConsole console;

        private readonly SpriteBatch spriteBatch;

        private readonly IStringDrawer stringDrawer;

        public GameConsoleComponent(
            GameEngine engine,
            GameConsole console,
            GameConsoleProcessor processor,
            SpriteBatch spriteBatch,
            IStringDrawer stringDrawer)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            if (console == null)
            {
                throw new ArgumentNullException(nameof(console));
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

            this.console   = console;
            this.Processer = processor;

            this.Processer.Opened += (s, e) => this.Renderer.Open();
            this.Processer.Closed += (s, e) => this.Renderer.Close();

            this.spriteBatch  = spriteBatch;
            this.stringDrawer = stringDrawer;

            var builtinCommands = new IConsoleCommand[]
            {
                new ExitCommand(engine),
                new HelpCommand()
            };

            GameConsoleSettings.Commands.AddRange(builtinCommands);
        }

        public bool IsOpen => this.Renderer.IsOpened;

        /// <remarks>
        /// Won't be added to Game.Components
        /// </remarks>
        internal GameConsoleProcessor Processer { get; }

        /// <remarks>
        /// Won't be added to Game.Components
        /// </remarks>
        internal GameConsoleRenderer Renderer { get; private set; }
    }

    #region DrawableComponent

    internal partial class GameConsoleComponent
    {
        public override void Initialize()
        {
            this.Processer.Initialize();
            this.Renderer = new GameConsoleRenderer((GameEngine)this.Game, this.Processer, this.spriteBatch, this.stringDrawer);

            // Renderer is just constructed. Commands depend on renderer has to 
            // be added now.
            var builtinCommands = new IConsoleCommand[]
            {
                new ClearCommand(this.Processer, this.Renderer)
            };

            GameConsoleSettings.Commands.AddRange(builtinCommands);

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            if (!this.console.Enabled)
            {
                return;
            }

            this.spriteBatch.Begin();

            this.Renderer.Draw(gameTime);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime time)
        {
            if (!this.console.Enabled)
            {
                return;
            }

            this.Processer.Update(time);
            this.Renderer .Update(time);

            base.Update(time);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Renderer ?.Dispose();
                this.Processer?.Dispose();

                // Should not set to null, for it is a static member
                GameConsoleSettings.Commands.Clear();
            }

            base.Dispose(disposing);
        }
    }

    #endregion

    #region Operations

    internal partial class GameConsoleComponent
    {
        public void WriteLine(string buffer, OutputType bufferType = OutputType.Output)
        {
            if (GameConsoleSettings.Settings.OpenOnWrite)
            {
                this.Processer.OpenConsole();
            }

            this.Processer.AddToOutput(buffer, bufferType);
        }
    }

    #endregion
}