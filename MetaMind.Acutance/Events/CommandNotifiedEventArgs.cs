namespace MetaMind.Acutance.Events
{
    using System;

    using MetaMind.Acutance.Concepts;

    public class CommandNotifiedEventArgs : EventArgs
    {
        public readonly Command NotifiedCommand;

        public CommandNotifiedEventArgs(Command notifiedCommand)
        {
            this.NotifiedCommand = notifiedCommand;
        }
    }
}