namespace MetaMind.Engine.Guis.Console
{
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Console.Commands;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class GameConsoleComponent : DrawableGameComponent
    {
        public bool IsOpen
        {
            get
            {
                return this.renderer.IsOpen;
            }
        }

        private readonly GameConsole console;
        private readonly SpriteBatch spriteBatch;
        private readonly InputProcessor inputProcesser;
        private readonly Renderer renderer;

        public GameConsoleComponent(GameConsole console, Game game, SpriteBatch spriteBatch, IStringDrawer stringDrawer)
            : base(game)
        {
            this.console = console;
            this.spriteBatch = spriteBatch;

            this.inputProcesser = new InputProcessor(new CommandProcesser(), game.Window);

            this.inputProcesser.Open += (s, e) => this.renderer.Open();
            this.inputProcesser.Close += (s, e) => this.renderer.Close();

            this.renderer = new Renderer(game, spriteBatch, stringDrawer, this.inputProcesser);
            var inbuiltCommands = new IConsoleCommand[] { new ClearCommand(this.inputProcesser), new ExitCommand(game), new HelpCommand() };
            GameConsoleOptions.Commands.AddRange(inbuiltCommands);
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

        public void WriteLine(string text)
        {
            this.inputProcesser.AddToOutput(text);
        }
    }
}