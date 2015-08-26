namespace MetaMind.Engine.Guis.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;
    using Commands;
    using Components.Fonts;

    using Microsoft.Xna.Framework.Input;

    using Keys = Microsoft.Xna.Framework.Input.Keys;

    internal class GameConsoleProcessor : IDisposable
    {
        private const int Backspace = 8;

        private const int Enter = 13;

        private const int Tab = 9;

        private readonly CommandProcesser commandProcesser;

        private bool isActive;

        private bool isHandled;

        private Form form;

        public GameConsoleProcessor(CommandProcesser commandProcesser)
        {
            if (commandProcesser == null)
            {
                throw new ArgumentNullException(nameof(commandProcesser));
            }

            this.commandProcesser = commandProcesser;

            this.CommandHistory   = new CommandHistory();

            this.Out    = new List<OutputLine>();
            this.Buffer = new OutputLine("", OutputLineType.Buffer);

            this.isActive = false;
        }

        ~GameConsoleProcessor()
        {
            this.Dispose();
        }

        #region Console Content

        public CommandHistory CommandHistory { get; set; }

        public OutputLine Buffer { get; set; }

        public List<OutputLine> Out { get; set; }

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

        #endregion

        #region Form

        public void HookApplicationForm()
        {
            this.form = this.ObtainApplicationForm();

            this.form.KeyPress += this.FormKeyPress;
            this.form.KeyDown += this.FormKeyDown;
        }

        /// <summary>
        /// Force application to find current app window controls with System.Windows.Forms (Windows)
        /// </summary>
        /// <remarks>
        /// Has to be called after GraphicsManager initialization, because by then the the windows 
        /// form is constructed.
        /// </remarks>>
        private Form ObtainApplicationForm()
        {
            return (Form)Control.FromHandle(Application.OpenForms[0].Handle);
        }

        #endregion

        #region Input

        private void FormKeyPress(object sender, KeyPressEventArgs e)
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
                    this.BackspaceBuffer();

                    break;

                case (char)Keys.Enter:
                    this.ExecuteBuffer();

                    break;

                case (char)Keys.Tab:
                    this.AutoComplete();

                    break;

                default:
                    this.AddToBuffer(e.KeyChar);

                    break;
            }
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.V) &&
                keyboard.IsKeyDown(Keys.LeftControl))
            {
                this.PasteFromClipboard();
            }

            if (e.KeyValue == GameConsoleSettings.Settings.ToggleKey)
            {
                this.ToggleConsole();
                this.isHandled = true;
            }

            switch (e.KeyValue)
            {
                case 38:
                    this.Buffer.Output = this.CommandHistory.Previous();
                    break;
                case 40:
                    this.Buffer.Output = this.CommandHistory.Next();
                    break;
            }
        }

        #endregion

        #region Operations

        public void AddToOutput(string buffer, OutputLineType bufferType)
        {
            foreach (var line in buffer.Split('\n'))
            {
                this.Out.Add(new OutputLine(line, bufferType));
            }
        }

        public void AddToBuffer(string text)
        {
            var lines = text.Split('\n').Where(line => line != "").ToArray();
            int i;
            for (i = 0; i < lines.Length - 1; i++)
            {
                var line = lines[i];
                this.Buffer.Output += line;
                this.ExecuteBuffer();
            }

            this.Buffer.Output += lines[i];
        }

        private void AddToBuffer(char c)
        {
            if (GameConsoleSettings.Settings.Font.IsPrintable(c))
            {
                this.Buffer.Output += c;
            }
        }

        public void AutoComplete()
        {
            var lastSpacePosition = this.Buffer.Output.LastIndexOf(' ');

            var textToMatch = lastSpacePosition < 0
                                  ? this.Buffer.Output
                                  : this.Buffer.Output.Substring(
                                      lastSpacePosition + 1,
                                      this.Buffer.Output.Length - lastSpacePosition - 1);

            var match = this.MatchingCommand(textToMatch);
            if (match == null)
            {
                return;
            }

            var restCommand = match.Name.Substring(textToMatch.Length);
            this.Buffer.Output += restCommand + " ";
        }

        private void BackspaceBuffer()
        {
            if (this.Buffer.Output.Length > 0)
            {
                this.Buffer.Output = this.Buffer.Output.Substring(0, this.Buffer.Output.Length - 1);
            }
        }

        private void ExecuteBuffer()
        {
            if (this.Buffer.Output.Length == 0)
            {
                return;
            }

            var output = this.commandProcesser.Process(this.Buffer.Output).Split('\n').Where(l => l != "");
            this.Out.Add(new OutputLine(this.Buffer.Output, OutputLineType.Buffer));
            foreach (var line in output)
            {
                this.Out.Add(new OutputLine(line, OutputLineType.Output));
            }

            this.CommandHistory.Add(this.Buffer.Output);
            this.Buffer.Output = "";
        }

        private void PasteFromClipboard()
        {
            // Thread apartment must be in Single-Threaded for the clipboard to work
            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
            {
                this.AddToBuffer(Clipboard.GetText());
            }
        }

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

        #region Auto Completion

        private IConsoleCommand MatchingCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                return null;
            }

            var matchingCommands = GameConsoleSettings.Commands.Where(c => c.Name != null && c.Name.StartsWith(command));
            return matchingCommands.FirstOrDefault();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (this.form != null)
            {
                this.form.KeyDown -= this.FormKeyDown;
                this.form.KeyPress -= this.FormKeyPress;

                this.form = null;
            }
        }

        #endregion
    }
}