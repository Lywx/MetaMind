namespace MetaMind.Acutance.Concepts
{
    using System;

    public class CommandCreatedEventArgs : EventArgs
    {
        public readonly string Name;

        public readonly string Path;

        public readonly int Minutes;

        public CommandCreatedEventArgs(string name, string path, int minutes)
        {
            this.Name    = name;
            this.Path    = path;
            this.Minutes = minutes;
        }
    }
}