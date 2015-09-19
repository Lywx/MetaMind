namespace MetaMind.Engine.Console
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;
    using Component.Font;
    using Processors;

    internal class BufferController : GameModuleComponent<GameConsole, GameConsoleSettings, GameConsoleLogic, GameConsoleVisual>
    {
        #region Constructors

        public BufferController(GameConsole module, GameEngine engine)
            : base(module, engine)
        {
            this.Input   = new CommandLine(string.Empty, CommandType.Input);
            this.Output  = new List<CommandLine>();
            this.History = new CommandHistory();
        }

        #endregion

        public CommandLine Input { get; set; }

        public CommandHistory History { get; set; }

        public List<CommandLine> Output { get; set; }

        #region Input Operations

        public void InputAdd(string text, ICommandProcessor processor)
        {
            var lines = text.Split('\n')
                            .Where(line => !string.IsNullOrEmpty(line))
                            .ToArray();
            int i;
            for (i = 0; i < lines.Length - 1; ++i)
            {
                var line = lines[i];
                this.Input.Command += line;

                this.InputExecute(processor);
            }

            this.Input.Command += lines[i];
        }

        public void InputAdd(char c)
        {
            if (this.Settings.Font.IsPrintable(c))
            {
                this.Input.Command += c;
            }
        }

        public void InputComplete(ICommandProcessor processor)
        {
            var commandToMatch = this.CommandToMatch(this.Input.Command.LastIndexOf(' '));

            var match = processor.Match(commandToMatch);
            if (match == null)
            {
                return;
            }

            // Fill out the incomplete command
            var restCommand = match.Name.Substring(commandToMatch.Length);
            this.Input.Command += restCommand + " ";
        }

        public void InputBackspace()
        {
            if (this.Input.Command.Length > 0)
            {
                this.Input.Command = this.Input.Command.Substring(0, this.Input.Command.Length - 1);
            }
        }

        public void InputExecute(ICommandProcessor processor)
        {
            if (this.Input.Command.Length == 0)
            {
                return;
            }

            // Add buffer to output before execution
            this.Output.Add(new CommandLine(this.Input.Command, CommandType.Input));

            // Add buffer to history
            this.History.Add(this.Input.Command);

            // Process buffer content
            var output = processor.Process(this.Input.Command)
                                  .Split('\n')
                                  .Where(l => !string.IsNullOrEmpty(l));

            // Add processed result to output
            foreach (var line in output)
            {
                this.Output.Add(new CommandLine(line, CommandType.Output));
            }

            // Clear buffer after processing
            this.Input.Command = "";
        }

        public void InputPaste(ICommandProcessor processor)
        {
            // Thread apartment must be in Single-Threaded for the clipboard to work
            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
            {
                this.InputAdd(Clipboard.GetText(), processor);
            }
        }

        #endregion

        #region Input History Operations

        public void HistoryPrevious()
        {
            this.Input.Command = this.History.Previous();
        }

        public void HistoryNext()
        {
            this.Input.Command = this.History.Next();
        }

        public void HistoryReset()
        {
            this.History.Reset();
        }

        #endregion

        #region Output Operations

        public void OutputAdd(string command, CommandType type)
        {
            foreach (var line in command.Split('\n'))
            {
                this.Output.Add(new CommandLine(line, type));
            }
        }

        public void OutputClear()
        {
            this.Output.Clear();
        }

        #endregion

        #region Helpers

        private string CommandToMatch(int lastSpaceIndex)
        {
            // Return the last word
            return lastSpaceIndex < 0
                       ? this.Input.Command
                       : this.Input.Command.Substring(
                           lastSpaceIndex + 1,
                           this.Input.Command.Length - lastSpaceIndex - 1);
        }

        #endregion
    }
}