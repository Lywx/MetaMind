namespace MetaMind.Engine.Service.Console
{
    using System;
    using System.Collections.Generic;
    using Commands;
    using Commands.Coreutils;
    using Components.Graphics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Processors;

    public class GameConsole : GameMvcComponent<GameConsoleSettings, GameConsoleLogic, GameConsoleVisual>
    {
        #region Constructors and Finalizer

        public GameConsole(GameConsoleSettings settings, GameEngine engine, SpriteBatch spriteBatch, IRenderer renderer)
            : base(settings, engine)
        {
            if (spriteBatch == null)
            {
                throw new ArgumentNullException(nameof(spriteBatch));
            }

            if (renderer == null)
            {
                throw new ArgumentNullException(nameof(renderer));
            }

            this.Logic = new GameConsoleLogic(this, engine, new CommandProcessor(this));
            this.Logic.Opened += (s, e) => this.Visual.Open();
            this.Logic.Closed += (s, e) => this.Visual.Close();

            this.Visual = new GameConsoleVisual(this, engine, spriteBatch, renderer);
        }

        #endregion

        #region Console States

        /// <summary>
        ///     Indicates whether the console is currently opened
        /// </summary>
        public bool IsOpen => this.Visual.IsOpened;

        #endregion

        #region Console Customization

        public List<IConsoleCommand> Commands => this.Settings.Commands;

        public ICommandProcessor Processor
        {
            get { return this.Logic.Processor; }
            set { this.Logic.Processor = value; }
        }

        #endregion

        #region Initialization

        public override void Initialize()
        {
            this.Logic .Initialize();
            this.Visual.Initialize();

            this.InitializeCommands();
            base.Initialize();
        }

        private void InitializeCommands()
        {
            var builtinCommands = new IConsoleCommand[]
            {
                // Console operations
                new ClearCommand(this),
                new HelpCommand(this),

                // Engine operations
                new ExitCommand(),
                new RestartCommand(),
#if DEBUG
                new ResetCommand(),
#endif
            };

            this.Commands.AddRange(builtinCommands);
        }

        #endregion

        protected override void LoadContent()
        {
            base.LoadContent();

            this.Settings.Font = this.Interop.Asset.Fonts["Lucida Console Regular"];
        }

        #region Update  

        public override void Update(GameTime time)
        {
            this.UpdateInput(this.Input, time);
            base.Update(time);
        }

        #endregion

        #region Command Operations

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

        #endregion

        #region Buffer Operations

        public void ClearOutput()
        {
            this.Logic.OutputClear();
            this.Visual.ResetCommandPosition();
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
                this.WriteLine(buffer, CommandType.Output);
            }
            else if (string.Compare(channel, "DEBUG", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.WriteLine(buffer, CommandType.Debug);
            }
            else if (string.Compare(channel, "ERROR", StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.WriteLine(buffer, CommandType.Error);
            }
        }

        private void WriteLine(string buffer, CommandType bufferType)
        {
            this.Logic.WriteLine(buffer, bufferType);
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Visual?.Dispose();
                this.Logic ?.Dispose();

                this.Commands.Clear();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}