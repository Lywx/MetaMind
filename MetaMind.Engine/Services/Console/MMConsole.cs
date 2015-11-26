namespace MetaMind.Engine.Services.Console
{
    using System;
    using System.Collections.Generic;
    using Commands;
    using Commands.Coreutils;
    using Components;
    using Components.Graphics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Processors;

    public class MMConsole : MMMVCComponent<GameConsoleSettings, MMConsoleController, MMConsoleRenderer>
    {
        #region Constructors and Finalizer

        public MMConsole(GameConsoleSettings settings, MMEngine engine, SpriteBatch spriteBatch, IMMRenderer renderer)
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

            this.Controller = new MMConsoleController(this, engine, new CommandProcessor(this));
            this.Controller.Opened += (s, e) => this.Renderer.Open();
            this.Controller.Closed += (s, e) => this.Renderer.Close();

            this.Renderer = new MMConsoleRenderer(this, engine, spriteBatch, renderer);
        }

        #endregion

        #region Console States

        /// <summary>
        ///     Indicates whether the console is currently opened
        /// </summary>
        public bool IsOpen => this.Renderer.IsOpened;

        #endregion

        #region Console Customization

        public List<IConsoleCommand> Commands => this.Settings.Commands;

        public ICommandProcessor Processor
        {
            get { return this.Controller.Processor; }
            set { this.Controller.Processor = value; }
        }

        #endregion

        #region Initialization

        public override void Initialize()
        {
            this.Controller .Initialize();
            this.Renderer.Initialize();

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
            this.UpdateInput(time);
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
            this.Controller.OutputClear();
            this.Renderer.ResetCommandPosition();
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

                return;
            }

            // Ignore case
            if (string.Compare(
                channel,
                "DEBUG",
                StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.WriteLine(buffer, CommandType.Debug);

                return;
            }

            // Ignore case
            if (string.Compare(
                channel,
                "ERROR",
                StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.WriteLine(buffer, CommandType.Error);
            }
        }

        private void WriteLine(string buffer, CommandType bufferType)
        {
            this.Controller.WriteLine(buffer, bufferType);
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Renderer?.Dispose();
                this.Controller ?.Dispose();

                this.Commands.Clear();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}