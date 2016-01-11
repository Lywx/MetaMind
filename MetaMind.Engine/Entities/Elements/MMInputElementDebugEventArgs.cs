namespace MetaMind.Engine.Entities.Elements
{
    using System;

    public class MMInputElementDebugEventArgs : EventArgs
    {
        public MMInputElementDebugEventArgs(MMInputElementDebugEvent elementEvent)
        {
            this.ElementEvent = elementEvent;
        }

        public MMInputElementDebugEvent ElementEvent { get; }
    }
}