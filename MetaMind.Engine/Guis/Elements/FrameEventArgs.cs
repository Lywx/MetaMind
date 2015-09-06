namespace MetaMind.Engine.Guis.Elements
{
    using System;

    public class FrameEventArgs : EventArgs
    {
        private readonly FrameEventType type;

        public FrameEventArgs(FrameEventType type)
        {
            this.type = type;
        }

        public FrameEventType Type => this.type;
    }
}