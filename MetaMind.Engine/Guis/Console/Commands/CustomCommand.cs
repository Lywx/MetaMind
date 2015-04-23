namespace MetaMind.Engine.Guis.Console.Commands
{
    using System;

    class CustomCommand:IConsoleCommand
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        private Func<string[], string> action;

        public CustomCommand(string name, Func<string[], string> action, string description)
        {
            this.Name = name;
            this.Description = description;
            this.action = action;
        }
        public string Execute(string[] arguments)
        {
            return this.action(arguments);
        }
    }
}
