namespace MetaMind.Acutance.Concepts
{
    using System;

    public class CallCreatedEventArgs : EventArgs
    {
        public readonly string Name;

        public readonly string Path;

        public readonly int Minutes;

        public CallCreatedEventArgs(string name, string path, int minutes)
        {
            this.Name    = name;
            this.Path    = path;
            this.Minutes = minutes;
        }
    }
}