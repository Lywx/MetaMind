namespace MetaMind.Engine.Core.Entity.Input
{
    using System;

    public class MMElementEventArgs : EventArgs
    {
        public MMElementEventArgs(MMElementEvent @event)
        {
            this.Event = @event;
        }

        public MMElementEvent Event { get; }
    }
}