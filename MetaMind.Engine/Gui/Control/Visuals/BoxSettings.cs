namespace MetaMind.Engine.Gui.Control.Visuals
{
    using System;
    using Microsoft.Xna.Framework;

    public class BoxSettings
    {
        public Func<Color> Color = () => Microsoft.Xna.Framework.Color.White;

        public Func<bool> ColorFilled = () => false;

        public Func<Rectangle> Bounds = () => Rectangle.Empty;

        public BoxSettings(Func<Rectangle> bounds)
        {
            this.Bounds = bounds;
        }
    }
}