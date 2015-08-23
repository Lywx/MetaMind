namespace MetaMind.Engine.Guis.Console
{
    using System;
    using Components.Fonts;
    using Commands;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal partial class GameConsoleComponent : DrawableGameComponent
    {
        private readonly GameConsole console;

        private readonly SpriteBatch spriteBatch;

        private readonly IStringDrawer stringDrawer;

        private readonly GameConsoleProcessor consoleProcesser;

        private GameConsoleRenderer consoleRenderer;

        public GameConsoleComponent(GameEngine engine, GameConsole console, SpriteBatch spriteBatch, IStringDrawer stringDrawer)
            : base(engine)
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

            this.console = console;

            this.spriteBatch  = spriteBatch;
            this.stringDrawer = stringDrawer;

            this.consoleProcesser = new GameConsoleProcessor(new CommandProcesser());
            this.consoleProcesser.Opened += (s, e) => this.consoleRenderer.Open();
            this.consoleProcesser.Closed += (s, e) => this.consoleRenderer.Close();

            var builtinCommands = new IConsoleCommand[]
            {
                new ExitCommand(engine),
                new HelpCommand()
            };

            GameConsoleSettings.Commands.AddRange(builtinCommands);
        }

        public bool IsOpen => this.consoleRenderer.IsOpened;
    }

    #region DrawableComponent

    internal partial class GameConsoleComponent
    {
        protected override void LoadContent()
        {
            this.consoleRenderer = new GameConsoleRenderer((GameEngine)this.Game, this.spriteBatch, this.stringDrawer, this.consoleProcesser);
            this.consoleProcesser.HookApplicationForm();

            // Renderer is just constructed. Commands depend on renderer has to 
            // be added now.
            var builtinCommands = new IConsoleCommand[]
            {
                new ClearCommand(this.consoleProcesser, this.consoleRenderer)
            };

            GameConsoleSettings.Commands.AddRange(builtinCommands);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            if (!this.console.Enabled)
            {
                return;
            }

            this.spriteBatch.Begin();

            this.consoleRenderer.Draw(gameTime);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (!this.console.Enabled)
            {
                return;
            }

            this.consoleRenderer.Update(gameTime);
            base                .Update(gameTime);
        }
    }

    #endregion

    #region Operations

    internal partial class GameConsoleComponent
    {
        public void WriteLine(string buffer, OutputLineType bufferType = OutputLineType.Output)
        {
            if (GameConsoleSettings.Settings.OpenOnWrite)
            {
                this.consoleProcesser.OpenConsole();
            }

            this.consoleProcesser.AddToOutput(buffer, bufferType);
        }
    }

    #endregion
}