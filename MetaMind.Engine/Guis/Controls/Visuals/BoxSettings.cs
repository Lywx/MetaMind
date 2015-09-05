namespace MetaMind.Engine.Guis.Controls.Visuals
{
    using System;
    using Microsoft.Xna.Framework;

    public class BoxSettings
    {
        public Func<Color> Color = () => Microsoft.Xna.Framework.Color.White;

        public Func<Rectangle> Bounds = () => Rectangle.Empty;

        public Func<bool> ColorFilled = () => false;

        public BoxSettings(Func<Rectangle> bounds)
        {
            this.Bounds = bounds;
        }
    }
}