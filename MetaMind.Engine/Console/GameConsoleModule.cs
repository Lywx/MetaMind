namespace MetaMind.Engine.Console
{
    using System;
    using Commands;
    using Commands.Core;
    using Components.Fonts;
    using Microsoft.Xna.Framework.Graphics;
    using Processors;

    internal partial class GameConsoleModule : GameModule<GameConsoleSettings, GameConsoleLogic, GameConsoleVisual>
    {
        private readonly GameConsole console;

        public GameConsoleModule(GameConsole console, GameConsoleSettings settings, ICommandProcessor processor, GameEngine engine, SpriteBatch spriteBatch, IStringDrawer stringDrawer)
            : base(settings, engine)
        {
            if (console == null)
            {
                throw new ArgumentNullException(nameof(console));
            }

            this.console = console;
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

            this.Logic = new GameConsoleLogic(this, engine, processor);
            this.Logic.Opened += (s, e) => this.Visual.Open();
            this.Logic.Closed += (s, e) => this.Visual.Close();

            this.Visual = new GameConsoleVisual(this, engine, spriteBatch, stringDrawer);

            var builtinCommands = new IConsoleCommand[]
            {
                new ExitCommand(this.Engine),
                new HelpCommand(this.console)
            };

            this.Settings.Commands.AddRange(builtinCommands);
        }

        public bool IsOpen => this.Visual.IsOpened;

        public bool Enabled => this.console.Enabled;
    }

    #region DrawableComponent

    internal partial class GameConsoleModule
    {
        public override void Initialize()
        {
            this.Logic .Initialize();
            this.Visual.Initialize();

            // visual is just constructed. Commands depend on visual has to 
            // be added now.
            var builtinCommands = new IConsoleCommand[]
            {
                new ClearCommand(this)
            };

            this.Settings.Commands.AddRange(builtinCommands);

            base.Initialize();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Visual?.Dispose();
                this.Logic ?.Dispose();

                this.Settings.Commands.Clear();
            }

            base.Dispose(disposing);
        }
    }

    #endregion

    #region Operations

    internal partial class GameConsoleModule
    {
        public void WriteLine(string buffer, CommandType bufferType = CommandType.Output)
        {
            this.Logic.WriteLine(buffer, bufferType);
        }

        public void Clear()
        {
            this.Logic.ClearOuput();
            this.Visual.ResetCommandPosition();
        }
    }

    #endregion
}