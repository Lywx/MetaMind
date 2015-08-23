namespace MetaMind.Engine.Guis.Console.Commands
{
    using System.Linq;
    using System.Text;

    internal class HelpCommand : IConsoleCommand
    {
        public string Name => "help";

        public string Description => "Displays the command description";

        public string Execute(string[] arguments)
        {
            if (arguments != null && arguments.Length >= 1)
            {
                var command = GameConsoleSettings.Commands.FirstOrDefault(c => c.Name != null && c.Name == arguments[0]);
                if (command != null)
                {
                    return $"{command.Name}: {command.Description}\n";
                }

                return "ERROR: Invalid command '" + arguments[0] + "'";
            }

            GameConsoleSettings.Commands.Sort(new CommandComparer());

            var pad  = GameConsoleSettings.Commands.Max(command => command.Name.Length);
            var help = new StringBuilder();
            foreach (var command in GameConsoleSettings.Commands)
            {
                help.Append(command.Name.PadRight(pad) + " - " + $"{command.Description}\n");
            }

            return help.ToString();
        }
    }
}