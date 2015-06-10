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

        private readonly InputProcessor inputProcesser;

        private Renderer renderer;

        public GameConsoleComponent(GameEngine engine, GameConsole console, SpriteBatch spriteBatch, IStringDrawer stringDrawer)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException("engine");
            }

            if (spriteBatch == null)
            {
                throw new ArgumentNullException("spriteBatch");
            }

            if (stringDrawer == null)
            {
                throw new ArgumentNullException("stringDrawer");
            }

            this.console      = console;
            this.spriteBatch  = spriteBatch;
            this.stringDrawer = stringDrawer;

            this.inputProcesser = new InputProcessor(new CommandProcesser());
            this.inputProcesser.Open += (s, e) => this.renderer.Open();
            this.inputProcesser.Close += (s, e) => this.renderer.Close();

            var builtinCommands = new IConsoleCommand[]
            {
                new ClearCommand(this.inputProcesser),
                new ExitCommand(engine),
                new HelpCommand()
            };
            GameConsoleOptions.Commands.AddRange(builtinCommands);
        }

        public bool IsOpen
        {
            get
            {
                return this.renderer.IsOpen;
            }
        }

        #region DrawableComponent

        protected override void LoadContent()
        {
            this.inputProcesser.SetupForm();

            // Has to be constructed after the font is loaded.
            this.renderer = new Renderer((GameEngine)this.Game, this.spriteBatch, this.stringDrawer, this.inputProcesser);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            if (!this.console.Enabled)
            {
                return;
            }

            this.spriteBatch.Begin();

            this.renderer.Draw(gameTime);

            this.spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (!this.console.Enabled)
            {
                return;
            }

            this.renderer.Update(gameTime);
            base.Update(gameTime);
        }

        #endregion
    }

    internal partial class GameConsoleComponent
    {
        #region Operations

        public void WriteLine(string text)
        {
            this.inputProcesser.AddToOutput(text);
        }

        #endregion
    }
}