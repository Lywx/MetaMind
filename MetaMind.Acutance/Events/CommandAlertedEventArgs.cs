namespace MetaMind.Acutance.Events
{
    using System;

    using MetaMind.Acutance.Concepts;

    public class CommandAlertedEventArgs : EventArgs
    {
        public readonly CommandEntry Entry;

        public CommandAlertedEventArgs(CommandEntry entry)
        {
            this.Entry = entry;
        }
    }
}