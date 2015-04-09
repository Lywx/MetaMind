namespace MetaMind.Acutance.Events
{
    using System;

    using MetaMind.Acutance.Concepts;

    public class CommandAlertedEventArgs : EventArgs
    {
        public readonly Command Command;

        public CommandAlertedEventArgs(Command command)
        {
            this.Command = command;
        }
    }
}