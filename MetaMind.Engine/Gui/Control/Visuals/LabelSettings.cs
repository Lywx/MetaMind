namespace MetaMind.Engine.Gui.Control.Visuals
{
    using System;
    using Component.Font;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// The restorable part of label configuration.
    /// </summary>
    public class LabelSettings : ICloneable
    {
        public Color TextColor;

        public Font TextFont;

        public HoritonalAlignment TextHAlignment = HoritonalAlignment.Right;

        public VerticalAlignment TextVAlignment = VerticalAlignment.Center;

        public bool TextMonospaced = false;

        public float TextSize;

        public int TextLeading;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}