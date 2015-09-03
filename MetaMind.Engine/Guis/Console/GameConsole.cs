namespace MetaMind.Engine.Guis.Console
{
    using System;
    using System.Collections.Generic;
    using Commands;
    using Components.Fonts;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Processors;
    using Services;

    public class GameConsole : DrawableGameComponent, IDisposable
    {
        #region Constructors and Finalizer

        public GameConsole(
            GameConsoleSettings settings,
            GameEngine engine,
            SpriteBatch spriteBatch,
            IStringDrawer stringDrawer) 
            : base(engine)
        {
            this.Module = new GameConsoleModule(this, settings, new CommandProcessor(this), engine, spriteBatch, stringDrawer);
        }

        #endregion

        #region Dependency

        private GameConsoleModule Module { get; set; }

        protected IGameInputService GameInput => GameEngine.Service.Input;

        protected IGameGraphicsService GameGraphics => GameEngine.Service.Graphics;

        #endregion

        public List<IConsoleCommand> Commands => this.Module.Settings.Commands;

        /// <summary>
        ///     Indicates whether the console is currently opened
        /// </summary>
        public bool Opened => this.Module.IsOpen;

        public ICommandProcessor Processor
        {
            get { return this.Module.Logic.Processor; }
            set { this.Module.Logic.Processor = value; }
        }

        public override void Initialize()
        {
            this.Module.Initialize();
            base       .Initialize();
        }

        public override void Update(GameTime time)
        {
            this.Module.UpdateInput(this.GameInput, time);
            this.Module.Update(time);
            base       .Update(time);
        }

        public override void Draw(GameTime time)
        {
            this.Module.Draw(this.GameGraphics, time, byte.MaxValue);
            base       .Draw(time);
        }

        #region Operations

        /// <summary>
        /// Adds a new command to the console
        /// </summary>
        /// <param name="commands"></param>
        public void AddCommand(params IConsoleCommand[] commands)
        {
            this.Commands.AddRange(commands);
        }

        /// <summary>
        ///     Adds a new command to the console
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="action"></param>
        public void AddCommand(string name, Func<string[], string> action)
        {
            this.AddCommand(name, action, string.Empty);
        }

        /// <summary>
        ///     Adds a new command to the console
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        public void AddCommand(string name, Func<string[], string> action, string description)
        {
            this.Commands.Add(new CustomCommand(name, description, action));
        }

        /// <summary>
        ///     Write directly to the output stream of the console
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="channel">Channel to write to, include "", "DEBUG", "ERROR"</param>
        public void WriteLine(string buffer, string channel = "")
        {
            if (string.IsNullOrEmpty(channel))
            {
                this.Module.WriteLine(buffer);
            }
            else if (string.Compare(channel, "DEBUG", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.Module.WriteLine(buffer, CommandType.Debug);
            }
            else if (string.Compare(channel, "ERROR", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.Module.WriteLine(buffer, CommandType.Error);
            }
        }

        #endregion

        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!this.IsDisposed)
                {
                    this.Module.Dispose();
                }

                this.IsDisposed = true;
            }
        }

        #endregion
    }
}