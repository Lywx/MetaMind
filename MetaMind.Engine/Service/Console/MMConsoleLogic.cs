namespace MetaMind.Engine.Service.Console
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Commands;
    using NLog;
    using Processors;
    using KeyEventArgs = Components.Input.KeyEventArgs;
    using Keys = Microsoft.Xna.Framework.Input.Keys;

    public class MMConsoleLogic : MMMvcComponentLogic<MMConsole, GameConsoleSettings, MMConsoleLogic, MMConsoleVisual>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private bool isActive = false;

        private bool isHandled;

        #region Constructors and Finalizer

        public MMConsoleLogic(MMConsole module, MMEngine engine, ICommandProcessor processor) 
            : base(module)
        {
            if (processor == null)
            {
                throw new ArgumentNullException(nameof(processor));
            }

            this.Processor = processor;

            this.Buffer = new BufferController(module, engine);
        }

        ~MMConsoleLogic()
        {
            this.Dispose(true);
        }

        #endregion

        #region Dependency

        private ICommandProcessor processor;

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

                Logger.Info($"Processor set to {this.processor}");
            }
        }

        internal List<CommandLine> Output => this.Buffer.Output;

        internal CommandLine Input => this.Buffer.Input;

        internal BufferController Buffer { get; set; }

        #endregion

        #region Initialization

        public override void Initialize()
        {
            this.Engine.Input.Event.KeyDown  += this.InputEventKeyDown;
            this.Engine.Input.Event.KeyPress += this.InputEventKeyPress;

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

            this.InputReset();

            var keyboard = this.Engine.Input.State.Keyboard;

            switch (e.KeyChar)
            {
                case (char)Keys.Back:
                {
                    this.Buffer.InputBackspace();

                    break;
                }

                case (char)Keys.Enter:
                {
                    this.Buffer.InputExecute(this.Processor);

                    break;
                }

                case (char)Keys.Tab:
                {
                    this.Buffer.InputComplete(this.Processor);

                    break;
                }

                case (char)Keys.V:
                {
                    if (keyboard.CtrlDown)
                    {
                        this.Buffer.InputPaste(this.Processor);
                    }

                    goto default;
                }

                default:
                {
                    this.Buffer.InputAdd(e.KeyChar);

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
                    this.Buffer.HistoryPrevious();
                    break;
                case Keys.Down:
                    this.Buffer.HistoryNext();
                    break;
                case Keys.PageUp:
                    this.Visual.PageUp();
                    break;
                case Keys.PageDown:
                    this.Visual.PageDown();
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

        public void OutputClear()
        {
            this.Buffer.OutputClear();
        }

        private void InputReset()
        {
            this.Buffer.HistoryReset();
            this.Visual.PageReset();
        }

        internal void WriteLine(string buffer, CommandType bufferType = CommandType.Output)
        {
            if (this.Settings.OpenOnWrite)
            {
                this.OpenConsole();
            }

            this.Buffer.OutputAdd(buffer, bufferType);
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
                    this.Engine.Input.Event.KeyDown -= this.InputEventKeyDown;
                    this.Engine.Input.Event.KeyPress -= this.InputEventKeyPress;
                }

                this.IsDisposed = true;
            }
        }

        #endregion
    }
}