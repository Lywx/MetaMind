namespace MetaMind.Acutance.Concepts
{
    using System;

    public class CommandNotifiedEventArgs : EventArgs
    {
        public readonly CommandEntry NotifiedCommand;

        public CommandNotifiedEventArgs(CommandEntry notifiedCommand)
        {
            this.NotifiedCommand = notifiedCommand;
        }
    }
}