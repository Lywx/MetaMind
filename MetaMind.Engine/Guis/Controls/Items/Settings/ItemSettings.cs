namespace MetaMind.Engine.Guis.Controls.Items.Settings
{
    using System;
    using System.Runtime.Serialization;
    using Services;

    [DataContract]
    public class ItemSettings : ControlSettings, IItemSettings, ICloneable
    {
        protected ItemSettings()
        {
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        protected IGameGraphicsService Graphics => GameEngine.Service.Graphics;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}