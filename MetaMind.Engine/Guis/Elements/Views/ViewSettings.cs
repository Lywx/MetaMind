namespace MetaMind.Engine.Guis.Elements.Views
{
    using System;

    public class ViewSettings : ICloneable
    {
        public bool MouseEnabled     = true;

        public bool KeyboardEnabled  = true;

        public bool Exchangeable     = true;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}