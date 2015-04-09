namespace MetaMind.Engine.Components
{
    using System;

    public class ScreenSettings : ICloneable
    {
        public bool AlwaysDraw = true;

        public bool AlwaysUpdate = true;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}