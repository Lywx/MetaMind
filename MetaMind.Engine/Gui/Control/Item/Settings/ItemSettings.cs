namespace MetaMind.Engine.Gui.Control.Item.Settings
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class ItemSettings : ControlSettings, IItemSettings, ICloneable
    {
        protected ItemSettings()
        {
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}