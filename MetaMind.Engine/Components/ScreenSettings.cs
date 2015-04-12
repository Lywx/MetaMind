namespace MetaMind.Engine.Components
{
    using System;

    public class ScreenSettings : ICloneable
    {
        public bool IsAlwaysVisible = true;

        public bool IsAlwaysActive  = true;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}