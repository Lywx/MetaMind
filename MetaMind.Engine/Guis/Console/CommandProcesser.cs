namespace MetaMind.Engine.Guis.Console
{
    using System;
    using System.Linq;

    internal class CommandProcesser
    {
        public string Process(string buffer)
        {
            var commandName = GetCommandName(buffer);
            var arguments   = GetArguments(buffer);

            var command = GameConsoleSettings.Commands.FirstOrDefault(c => c.Name == commandName);
            if (command == null)
            {
                return "ERROR: Command not found";
            }

            string commandOutput;

            try
            {
                commandOutput = command.Execute(arguments);
            }
            catch (Exception ex)
            {
                commandOutput = "ERROR: " + ex.Message;
            }

            return commandOutput;
        }

        private static string GetCommandName(string buffer)
        {
            var firstSpace = buffer.IndexOf(' ');
            return buffer.Substring(0, firstSpace < 0 ? buffer.Length : firstSpace);
        }

        private static string[] GetArguments(string buffer)
        {
            var firstSpace = buffer.IndexOf(' ');
            if (firstSpace < 0)
            {
                return new string[0];
            }
            
            var args = buffer.Substring(firstSpace, buffer.Length - firstSpace).Split(' ');
            return args.Where(a => a != "").ToArray();
        }
    }
}
