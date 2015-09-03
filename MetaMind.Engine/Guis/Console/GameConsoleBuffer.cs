namespace MetaMind.Engine.Guis.Console
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;
    using Components.Fonts;
    using Processors;

    internal class GameConsoleBuffer : GameModuleComponent<GameConsoleModule, GameConsoleSettings, GameConsoleLogic, GameConsoleVisual>
    {
        #region Constructors

        public GameConsoleBuffer(GameConsoleModule module, GameEngine engine)
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

        public void AddInput(string text, ICommandProcessor processor)
        {
            var lines = text.Split('\n')
                            .Where(line => !string.IsNullOrEmpty(line))
                            .ToArray();
            int i;
            for (i = 0; i < lines.Length - 1; ++i)
            {
                var line = lines[i];
                this.Input.Command += line;
            }

            this.Input.Command += lines[i];

            this.ExecuteInput(processor);
        }

        public void AddInput(char c)
        {
            if (this.Settings.Font.IsPrintable(c))
            {
                this.Input.Command += c;
            }
        }

        public void AutoCompleteInput(ICommandProcessor processor)
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

        public void BackspaceInput()
        {
            if (this.Input.Command.Length > 0)
            {
                this.Input.Command = this.Input.Command.Substring(0, this.Input.Command.Length - 1);
            }
        }

        public void PrepareForInput()
        {
            this.History.Reset();
        }

        public void ExecuteInput(ICommandProcessor processor)
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

        public void PasteInput(ICommandProcessor processor)
        {
            // Thread apartment must be in Single-Threaded for the clipboard to work
            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
            {
                this.AddInput(Clipboard.GetText(), processor);
            }
        }

        #endregion

        #region Input History Operations

        public void PreviousInput()
        {
            this.Input.Command = this.History.Previous();
        }

        public void NextInput()
        {
            this.Input.Command = this.History.Next();
        }

        #endregion

        #region Output Operations

        public void AddToOutput(string command, CommandType type)
        {
            foreach (var line in command.Split('\n'))
            {
                this.Output.Add(new CommandLine(line, type));
            }
        }

        public void ClearOutput()
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