namespace MetaMind.Acutance.Events
{
    using System;

    using MetaMind.Acutance.Concepts;

    public class CommandAlertedEventArgs : EventArgs
    {
        private readonly Command alertCommand;

        public CommandAlertedEventArgs(Command alertCommand)
        {
            this.alertCommand = alertCommand;
        }

        public Command AlertCommand
        {
            get
            {
                return this.alertCommand;
            }
        }
    }
}