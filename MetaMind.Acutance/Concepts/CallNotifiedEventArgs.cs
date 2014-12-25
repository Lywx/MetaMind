namespace MetaMind.Acutance.Concepts
{
    using System;

    public class CallNotifiedEventArgs : EventArgs
    {
        public readonly CallEntry NotifiedCall;

        public CallNotifiedEventArgs(CallEntry notifiedCall)
        {
            this.NotifiedCall = notifiedCall;
        }
    }
}