using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MonoGameConsole
{
    using System.Windows.Forms;

    using MetaMind.Engine.Components.Fonts;

    using Keys = Microsoft.Xna.Framework.Input.Keys;

    internal class InputProcessor
    {
        public event EventHandler Open = delegate { };

        public event EventHandler Close = delegate { };

        public event EventHandler PlayerCommand = delegate { };

        public event EventHandler ConsoleCommand = delegate { };

        public CommandHistory CommandHistory { get; set; }

        public OutputLine Buffer { get; set; }

        public List<OutputLine> Out { get; set; }

        private const int BACKSPACE = 8;
        private const int ENTER = 13;
        private const int TAB = 9;
        private bool isActive, isHandled;
        private CommandProcesser commandProcesser;

        public InputProcessor(CommandProcesser commandProcesser, GameWindow window)
        {
            this.commandProcesser = commandProcesser;
            isActive = false;
            CommandHistory = new CommandHistory();
            Out = new List<OutputLine>();
            Buffer = new OutputLine("", OutputLineType.Command);

            Form winGameWindow = GetGameWindow();
            winGameWindow.KeyPress += form_KeyPress;
            winGameWindow.KeyDown += EventInput_KeyDown;
        }

        /// <summary>
        /// Force application to find current app window controls with System.Windows.Forms (Windows)
        /// </summary>
        /// <returns></returns>
        private Form GetGameWindow()
        {
            FormCollection winFormsWindow = Application.OpenForms;

            return (Form)Control.FromHandle(winFormsWindow[0].Handle);
        }

        private void form_KeyPress(Object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (this.isHandled)
            {
                this.isHandled = false;
                return;
            }

            if (!this.isActive)
            {
                return; // console is opened -> accept input
            }

            this.CommandHistory.Reset();

            switch (e.KeyChar)
            {
                case (char)Keys.Enter:
                    this.ExecuteBuffer();
                    break;

                case (char)Keys.Back:
                    if (this.Buffer.Output.Length > 0)
                    {
                        this.Buffer.Output = this.Buffer.Output.Substring(0, this.Buffer.Output.Length - 1);
                    }
                    break;

                case (char)Keys.Tab:
                    this.AutoComplete();
                    break;

                default:
                    if (IsPrintable(e.KeyChar))
                    {
                        this.Buffer.Output += e.KeyChar;
                    }
                    break;
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

        public void AddToOutput(string text)
        {
            if (GameConsoleOptions.Options.OpenOnWrite)
            {
                this.isActive = true;
                this.Open(this, EventArgs.Empty);
            }
            foreach (var line in text.Split('\n'))
            {
                this.Out.Add(new OutputLine(line, OutputLineType.Output));
            }
        }

        private void EventInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.V) && Keyboard.GetState().IsKeyDown(Keys.LeftControl)) // CTRL + V
            {
                if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA) //Thread Apartment must be in Single-Threaded for the Clipboard to work
                {
                    this.AddToBuffer(Clipboard.GetText());
                }
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

        private void ToggleConsole()
        {
            this.isActive = !this.isActive;
            if (this.isActive)
            {
                Open(this, EventArgs.Empty);
            }
            else
            {
                Close(this, EventArgs.Empty);
            }
        }

        private void ExecuteBuffer()
        {
            if (Buffer.Output.Length == 0)
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

        private void AutoComplete()
        {
            var lastSpacePosition = this.Buffer.Output.LastIndexOf(' ');
            var textToMatch = lastSpacePosition < 0
                                  ? this.Buffer.Output
                                  : this.Buffer.Output.Substring(
                                      lastSpacePosition + 1,
                                      this.Buffer.Output.Length - lastSpacePosition - 1);
            var match = GetMatchingCommand(textToMatch);
            if (match == null)
            {
                return;
            }
            var restOfTheCommand = match.Name.Substring(textToMatch.Length);
            this.Buffer.Output += restOfTheCommand + " ";
        }

        private static IConsoleCommand GetMatchingCommand(string command)
        {
            var matchingCommands = GameConsoleOptions.Commands.Where(c => c.Name != null && c.Name.StartsWith(command));
            return matchingCommands.FirstOrDefault();
        }

        private static bool IsPrintable(char letter)
        {
            return GameConsoleOptions.Options.Font.GetSprite().Characters.Contains(letter);
        }
    }
}