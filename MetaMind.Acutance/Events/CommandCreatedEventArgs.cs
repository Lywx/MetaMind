namespace MetaMind.Acutance.Events
{
    using System;

    public class CommandCreatedEventArgs : EventArgs
    {
        public readonly string Name;

        public readonly string Path;

        public readonly int Minutes;

        // TODO: add this to view
        public CommandCreatedEventArgs(string name, string path, int minutes)
        {
            this.Name    = name;
            this.Path    = path;
            this.Minutes = minutes;
        }
    }
}