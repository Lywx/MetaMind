namespace MetaMind.Engine.Guis.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;
    using Components.Fonts;
    using Microsoft.Xna.Framework;
    using Services;

    // Use Xna and MonoGame Keys implementation
    using KeyEventArgs = Components.Inputs.KeyEventArgs;
    using Keys = Microsoft.Xna.Framework.Input.Keys;

    internal class GameConsoleProcessor : GameControllableComponent, IDisposable
    {
        private bool isActive;

        private bool isHandled;

        #region Constructors and Destructor

        public GameConsoleProcessor(GameEngine engine, CommandProcessor commandProcessor) 
            : base(engine)
        {
            if (commandProcessor == null)
            {
                throw new ArgumentNullException(nameof(commandProcessor));
            }

            this.CommandProcessor = commandProcessor;
            this.CommandHistory = new CommandHistory();

            this.CommandOutput = new List<OutputLine>();
            this.CommandBuffer = new OutputLine("", OutputType.Buffer);

            this.isActive = false;
        }

        ~GameConsoleProcessor()
        {
            this.Dispose(true);
        }

        #endregion

        #region Console Content

        public CommandHistory CommandHistory { get; set; }

        public ICommandProcessor CommandProcessor { get; set; }

        public OutputLine CommandBuffer { get; set; }

        public List<OutputLine> CommandOutput { get; set; }

        #endregion

        #region Dependency

        protected IGameInputService GameInput { get; set; }

        #endregion

        #region Initialization

        public override void Initialize()
        {
            var engine = (GameEngine)this.Game;

            this.GameInput = engine.Input;

            this.GameInput.Event.KeyDown += this.InputEventKeyDown;
            this.GameInput.Event.KeyPress += this.InputEventKeyPress;

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

            this.CommandHistory.Reset();

            switch (e.KeyChar)
            {
                case (char)Keys.Back:
                    this.BufferBackspace();

                    break;

                case (char)Keys.Enter:
                    this.BufferExecute();

                    break;

                case (char)Keys.Tab:
                    this.BufferAutoComplete();

                    break;

                default:
                    this.AddToBuffer(e.KeyChar);

                    break;
            }
        }

        private void InputEventKeyDown(object sender, KeyEventArgs e)
        {
            var keyboard = this.GameInput.State.Keyboard;

            if (keyboard.CtrlDown && 
                keyboard.IsKeyTriggered(Keys.V))
            {
                this.BufferPaste();
            }

            if (e.KeyCode == GameConsoleSettings.Settings.ToggleKey)
            {
                this.ToggleConsole();
                this.isHandled = true;
            }

            switch (e.KeyCode)
            {
                case Keys.Up:
                    this.CommandBuffer.Output = this.CommandHistory.Previous();
                    break;
                case Keys.Down:
                    this.CommandBuffer.Output = this.CommandHistory.Next();
                    break;
                //case Keys.PageUp:
                //    this.
                //    break;
                //case Keys.PageDown:
                //    break;
            }
        }

        #endregion

        #region Update

        public override void UpdateInput(GameTime gameTime)
        {
        }

        #endregion

        #region Operations

        #region Buffer

        private void AddToBuffer(string text)
        {
            var lines = text.Split('\n').Where(line => line != "").ToArray();
            int i;
            for (i = 0; i < lines.Length - 1; i++)
            {
                var line = lines[i];
                this.CommandBuffer.Output += line;
                this.BufferExecute();
            }

            this.CommandBuffer.Output += lines[i];
        }

        private void AddToBuffer(char c)
        {
            if (GameConsoleSettings.Settings.Font.IsPrintable(c))
            {
                this.CommandBuffer.Output += c;
            }
        }

        private void BufferAutoComplete()
        {
            var lastSpacePosition = this.CommandBuffer.Output.LastIndexOf(' ');

            var textToMatch = lastSpacePosition < 0
                                  ? this.CommandBuffer.Output
                                  : this.CommandBuffer.Output.Substring(
                                      lastSpacePosition + 1,
                                      this.CommandBuffer.Output.Length
                                      - lastSpacePosition - 1);

            var match = this.CommandProcessor.Match(textToMatch);
            if (match == null)
            {
                return;
            }

            var restCommand = match.Name.Substring(textToMatch.Length);
            this.CommandBuffer.Output += restCommand + " ";
        }

        private void BufferBackspace()
        {
            if (this.CommandBuffer.Output.Length > 0)
            {
                this.CommandBuffer.Output = this.CommandBuffer.Output.Substring(0, this.CommandBuffer.Output.Length - 1);
            }
        }

        private void BufferExecute()
        {
            if (this.CommandBuffer.Output.Length == 0)
            {
                return;
            }

            // Add buffer to output before execution
            this.CommandOutput.Add(new OutputLine(this.CommandBuffer.Output, OutputType.Buffer));

            // Add buffer to history
            this.CommandHistory.Add(this.CommandBuffer.Output);

            // Process buffer content
            var output =
                this.CommandProcessor.Process(this.CommandBuffer.Output).
                     Split('\n').
                     Where(l => !string.IsNullOrEmpty(l));

            // Add processed result to output
            foreach (var line in output)
            {
                this.CommandOutput.Add(new OutputLine(line, OutputType.Output));
            }

            // Clear buffer after processing
            this.CommandBuffer.Output = "";
        }

        private void BufferPaste()
        {
            // Thread apartment must be in Single-Threaded for the clipboard to work
            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
            {
                this.AddToBuffer(Clipboard.GetText());
            }
        }

        #endregion

        #region Output

        public void AddToOutput(string buffer, OutputType bufferType)
        {
            foreach (var line in buffer.Split('\n'))
            {
                this.CommandOutput.Add(new OutputLine(line, bufferType));
            }
        }

        #endregion

        #region State

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

        #endregion

        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.IsDisposed)
                    {
                        this.GameInput.Event.KeyDown -= this.InputEventKeyDown;
                        this.GameInput.Event.KeyPress -= this.InputEventKeyPress;
                    }

                    this.IsDisposed = true;
                }
            }
            catch
            {
                // Ignored
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion


    }
}