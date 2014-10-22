using System;

namespace MetaMind.Engine.Guis.Elements.Frames
{
    public class FrameEventArgs : EventArgs
    {
        private readonly FrameEventType type;

        public FrameEventArgs(FrameEventType type)
        {
            this.type = type;
        }

        public FrameEventType Type
        {
            get { return type; }
        }
    }
}