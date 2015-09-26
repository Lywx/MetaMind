namespace MetaMind.Engine.Gui.Elements
{
    using System;

    public class ElementEventArgs : EventArgs
    {
        public ElementEventArgs(ElementEvent type)
        {
            this.Type = type;
        }

        public ElementEvent Type { get; }
    }
}