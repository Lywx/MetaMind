namespace MetaMind.Engine.Gui.Elements
{
    using System;

    public class MMElementEventArgs : EventArgs
    {
        public MMElementEventArgs(MMElementEvent type)
        {
            this.Type = type;
        }

        public MMElementEvent Type { get; }
    }
}