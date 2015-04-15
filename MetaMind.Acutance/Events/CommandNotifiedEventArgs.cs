namespace MetaMind.Acutance.Events
{
    using System;

    using MetaMind.Acutance.Concepts;

    public class CommandNotifiedEventArgs : EventArgs
    {
        private readonly Command notifiedCommand;

        public CommandNotifiedEventArgs(Command notifiedCommand)
        {
            this.notifiedCommand = notifiedCommand;
        }

        public Command NotifiedCommandCommand
        {
            get
            {
                return this.notifiedCommand;
            }
        }
    }
}