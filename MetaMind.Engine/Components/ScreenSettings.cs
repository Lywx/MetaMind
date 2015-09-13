namespace MetaMind.Engine.Components
{
    using System;

    public class ScreenSettings : ICloneable
    {
        public bool IsAlwaysVisible = false;

        /// <remarks>
        /// Helpful for debugging update issues
        /// </remarks>>
        public bool IsAlwaysActive  = true;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}