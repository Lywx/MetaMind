namespace MetaMind.Engine.Guis.Elements.Views
{
    using System;

    public class ViewSettings : ICloneable
    {
        public bool MouseEnabled     = true;

        public bool KeyboardEnabled  = true;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}