﻿namespace MetaMind.Engine.Guis.Console
{
    using System;
    using System.Linq;
    using Commands;

    public class CommandProcessor : ICommandProcessor
    {
        #region Auto Completion

        public IConsoleCommand Match(string incomplete)
        {
            if (string.IsNullOrEmpty(incomplete))
            {
                return null;
            }

            var matchingCommands = GameConsoleSettings.Commands
                                                      .Where(
                                                          c => c.Name != null &&
                                                               c.Name.StartsWith
                                                                   (incomplete));
            return matchingCommands.FirstOrDefault();
        }

        #endregion

        #region Command Processing

        public string Process(string buffer)
        {
            var arguments   = this.GetArguments(buffer);
            var commandName = this.GetCommandName(buffer);

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
            catch (Exception e)
            {
                commandOutput = "ERROR: " + e.Message;
            }

            return commandOutput;
        }

        private string GetCommandName(string buffer)
        {
            var firstSpace = buffer.IndexOf(' ');
            return 
                buffer.Substring(0, firstSpace < 0 ? buffer.Length : firstSpace);
        }

        private string[] GetArguments(string buffer)
        {
            var firstSpace = buffer.IndexOf(' ');
            if (firstSpace < 0)
            {
                return new string[0];
            }

            var args = buffer.Substring(firstSpace, buffer.Length - firstSpace).
                              Split(' ');

            return args.Where(a => a != "").ToArray();
        }


        #endregion
    }
}
