namespace MetaMind.Engine.Guis.Widgets.Items.Settings
{
    using System;
    using System.Runtime.Serialization;
    using Services;

    [DataContract]
    public class ItemSettings : WidgetSettings, IItemSettings, ICloneable
    {
        protected ItemSettings()
        {
            this.SetupService();
        }

        [OnDeserialized]
        private void SetupService(StreamingContext context)
        {
            this.SetupService();
        }

        private void SetupService()
        {
            this.GameGraphics = GameEngine.Service?.Graphics;
        }

        protected IGameGraphicsService GameGraphics { get; set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}