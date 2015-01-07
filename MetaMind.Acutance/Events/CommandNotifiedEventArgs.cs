namespace MetaMind.Acutance.Events
{
    using System;

    using MetaMind.Acutance.Concepts;

    public class CommandNotifiedEventArgs : EventArgs
    {
        public readonly CommandEntry NotifiedCommand;

        public CommandNotifiedEventArgs(CommandEntry notifiedCommand)
        {
            this.NotifiedCommand = notifiedCommand;
        }
    }
}