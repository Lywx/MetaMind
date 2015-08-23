namespace MetaMind.Engine.Guis.Console.Commands
{
    using System;

    internal class ClearCommand : IConsoleCommand
    {
        private readonly GameConsoleProcessor processor;

        private readonly GameConsoleRenderer renderer;

        public ClearCommand(GameConsoleProcessor processor, GameConsoleRenderer renderer)
        {
            if (processor == null)
            {
                throw new ArgumentNullException(nameof(processor));
            }

            if (renderer == null)
            {
                throw new ArgumentNullException(nameof(renderer));
            }

            this.processor = processor;
            this.renderer = renderer;
        }

        public string Name => "clear";

        public string Description => "Clears the console output";

        public string Execute(string[] arguments)
        {
            this.processor.Out.Clear();
            this.renderer.ResetCommandPosition();

            return "";
        }
    }
}
