namespace MetaMind.Engine.Gui.Controls.Labels
{
    using System;
    using Engine.Components.Content.Fonts;
    using Engine.Components.Graphics.Fonts;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// The restorable part of label configuration.
    /// </summary>
    public struct LabelSettings : ICloneable
    {
        #region Alignment

        public HoritonalAlignment HAlignment;

        public VerticalAlignment VAlignment;

        #endregion

        #region Layout

        public float Size;

        public int Leading;

        #endregion

        public Color Color;

        public Font Font;

        public bool Monospaced;

        public LabelSettings(
            Font font,
            Color color,
            float size,
            int leading,
            HoritonalAlignment halignment,
            VerticalAlignment valignment,
            bool monospaced)
        {
            this.Font  = font;
            this.Color = color;

            this.Size    = size;
            this.Leading = leading;

            this.HAlignment = halignment;
            this.VAlignment = valignment;

            this.Monospaced = monospaced;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}