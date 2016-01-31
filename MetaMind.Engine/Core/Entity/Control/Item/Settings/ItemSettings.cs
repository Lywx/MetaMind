namespace MetaMind.Engine.Core.Entity.Control.Item.Settings
{
    using System;
    using System.Runtime.Serialization;
    using Core.Settings;

    // TODO(Critical): Should I remove all the gui settings?
    [DataContract]
    public class ItemSettings : MMSettings, IItemSettings, ICloneable
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