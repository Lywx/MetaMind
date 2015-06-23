namespace MetaMind.Engine.Guis.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    using Components.Fonts;

    using Microsoft.Xna.Framework.Input;

    using Keys = Microsoft.Xna.Framework.Input.Keys;

    internal class InputProcessor
    {
        private const int Backspace = 8;

        private const int Enter = 13;

        private const int Tab = 9;

        private readonly CommandProcesser commandProcesser;

        private bool isActive;

        private bool isHandled;

        public InputProcessor(CommandProcesser commandProcesser)
        {
            this.commandProcesser = commandProcesser;
            this.CommandHistory = new CommandHistory();

            this.Out    = new List<OutputLine>();
            this.Buffer = new OutputLine("", OutputLineType.Command);

            this.isActive = false;
        }

        public CommandHistory CommandHistory { get; set; }

        public OutputLine Buffer { get; set; }

        public List<OutputLine> Out { get; set; }

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

        public void SetupForm()
        {
            var form = this.GameForm();
            form.KeyPress += this.FormKeyPress;
            form.KeyDown += this.FormKeyDown;
        }

        /// <summary>
        /// Force application to find current app window controls with System.Windows.Forms (Windows)
        /// </summary>
        /// <remarks>
        /// Has to be called after GraphicsManager initialization, because by then the the windows 
        /// form is constructed.
        /// </remarks>>
        private Form GameForm()
        {
            var forms = Application.OpenForms;

            return (Form)Control.FromHandle(forms[0].Handle);
        }

        #endregion

        #region Input

        private void FormKeyPress(Object sender, KeyPressEventArgs e)
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

            if (e.KeyValue == GameConsoleOptions.Options.ToggleKey)
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

        public void AddToOutput(string text)
        {
            foreach (var line in text.Split('\n'))
            {
                this.Out.Add(new OutputLine(line, OutputLineType.Output));
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
            if (GameConsoleOptions.Options.Font.IsPrintable(c))
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

            var restOfTheCommand = match.Name.Substring(textToMatch.Length);
            this.Buffer.Output += restOfTheCommand + " ";
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
            this.Out.Add(new OutputLine(this.Buffer.Output, OutputLineType.Command));
            foreach (var line in output)
            {
                this.Out.Add(new OutputLine(line, OutputLineType.Output));
            }

            this.CommandHistory.Add(this.Buffer.Output);
            this.Buffer.Output = "";
        }

        private void PasteFromClipboard()
        {
            // Thread Apartment must be in Single-Threaded for the Clipboard to work
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

            var matchingCommands = GameConsoleOptions.Commands.Where(c => c.Name != null && c.Name.StartsWith(command));
            return matchingCommands.FirstOrDefault();
        }

        #endregion
    }
}