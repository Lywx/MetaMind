namespace MetaMind.Engine.Console
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Components.Inputs;
    using NLog;
    using Processors;
    using KeyEventArgs = Components.Inputs.KeyEventArgs;
    using Keys = Microsoft.Xna.Framework.Input.Keys;

    internal class GameConsoleLogic : GameModuleLogic<GameConsoleModule, GameConsoleSettings, GameConsoleLogic, GameConsoleVisual>, IDisposable
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private bool isActive = false;

        private bool isHandled;

        #region Constructors and Finalizer

        public GameConsoleLogic(GameConsoleModule module, GameEngine engine, ICommandProcessor processor) 
            : base(module, engine)
        {
            if (processor == null)
            {
                throw new ArgumentNullException(nameof(processor));
            }

            this.Processor = processor;

            this.Buffer = new GameConsoleBuffer(module, engine);
        }

        ~GameConsoleLogic()
        {
            this.Dispose(true);
        }

        #endregion

        #region Dependency

        private ICommandProcessor processor;

        public List<CommandLine> Output => this.Buffer.Output;

        public CommandLine Input => this.Buffer.Input;

        protected internal ICommandProcessor Processor
        {
            get { return this.processor; }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                // Allow multiple time injection
                this.processor = value;

#if DEBUG
                logger.Info($"Processor set to {this.processor}");
#endif
            }
        }

        protected GameConsoleBuffer Buffer { get; set; }

        protected IInputState InputState => this.Engine.Input.State;

        protected IInputEvent InputEvent => this.Engine.Input.Event;

        #endregion

        #region Initialization

        public override void Initialize()
        {
            this.InputEvent.KeyDown  += this.InputEventKeyDown;
            this.InputEvent.KeyPress += this.InputEventKeyPress;

            base.Initialize();
        }

        #endregion

        #region Events

        public event EventHandler Opened = delegate { };

        public event EventHandler Closed = delegate { };

        private void OnOpened()
        {
            this.isActive = true;

            this.Opened(this, EventArgs.Empty);
        }

        private void OnClosed()
        {
            this.isActive = false;

            this.Closed(this, EventArgs.Empty);
        }

        private void InputEventKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (this.isHandled)
            {
                this.isHandled = false;

                return;
            }

            if (!this.isActive)
            {
                return; 
            }

            this.Buffer.PrepareForInput();

            var keyboard = this.InputState.Keyboard;

            switch (e.KeyChar)
            {
                case (char)Keys.Back:
                {
                    this.Buffer.BackspaceInput();

                    break;
                }

                case (char)Keys.Enter:
                {
                    this.Buffer.ExecuteInput(this.Processor);

                    break;
                }

                case (char)Keys.Tab:
                {
                    this.Buffer.AutoCompleteInput(this.Processor);

                    break;
                }

                case (char)Keys.V:
                {
                    if (keyboard.CtrlDown)
                    {
                        this.Buffer.PasteInput(this.Processor);
                    }

                    goto default;
                }

                default:
                {
                    this.Buffer.AddInput(e.KeyChar);

                    break;
                }
            }
        }

        private void InputEventKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == this.Settings.ToggleKey)
            {
                this.ToggleConsole();
                this.isHandled = true;
            }

            switch (e.KeyCode)
            {
                case Keys.Up:
                    this.Buffer.PreviousInput();
                    break;
                case Keys.Down:
                    this.Buffer.NextInput();
                    break;
                case Keys.PageUp:
                    this.Visual.ScrollUp();
                    break;
                case Keys.PageDown:
                    this.Visual.ScrollDown();
                    break;
            }
        }

        #endregion

        #region State Operations

        public void OpenConsole()
        {
            this.OnOpened();
        }

        public void CloseConsole()
        {
            this.OnClosed();
        }

        public void ToggleConsole()
        {
            if (!this.isActive)
            {
                this.OnOpened();
            }
            else
            {
                this.OnClosed();
            }
        }

        #endregion

        #region Buffer Operations

        public void ClearOuput()
        {
            this.Buffer.ClearOutput();
        }

        public void WriteLine(string buffer, CommandType bufferType = CommandType.Output)
        {
            if (this.Settings.OpenOnWrite)
            {
                this.OpenConsole();
            }

            this.Buffer.AddToOutput(buffer, bufferType);
        }

        #endregion

        #region IDisposable

        private bool IsDisposed { get; set; }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!this.IsDisposed)
                {
                    this.InputEvent.KeyDown -= this.InputEventKeyDown;
                    this.InputEvent.KeyPress -= this.InputEventKeyPress;
                }

                this.IsDisposed = true;
            }
        }

        #endregion
    }
}