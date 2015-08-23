namespace MetaMind.Engine.Guis.Widgets.Items.Settings
{
    using System;
    using System.Runtime.Serialization;
    using Services;

    [DataContract]
    public class ItemSettings : WidgetSettings, IItemSettings, ICloneable
    {
        private readonly GameVisualEntity visualEntity = new GameVisualEntity();

        protected IGameGraphicsService GameGraphics => this.visualEntity.GameGraphics;

        protected IGameInteropService GameInterop => this.visualEntity.GameInterop;

        protected IGameNumericalService GameNumerical => this.visualEntity.GameNumerical;

        public ItemSettings()
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