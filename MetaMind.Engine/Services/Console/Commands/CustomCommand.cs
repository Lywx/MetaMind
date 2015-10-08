namespace MetaMind.Engine.Services.Console.Commands
{
    using System;

    internal class CustomCommand : IConsoleCommand
    {
        public CustomCommand(string name, string description, Func<string[], string> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            this.Name        = name;
            this.Description = description;
            this.Action      = action;
        }

        public string Name { get; }

        public string Description { get; }

        public Func<string[], string> Action { get; }

        public string Execute(string[] arguments)
        {
            return this.Action(arguments);
        }
    }
}
