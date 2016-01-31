namespace MetaMind.Engine.Core.Entity.Control.Item
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class MMViewItemRenderSettings : ICloneable
    {
        #region Geometry

        public MMViewItemRenderState<MMMargin> Margin;

        public MMViewItemRenderState<Point> Size;

        #endregion

        public MMViewItemRenderState<Color> Color;

        public MMViewItemRenderState<Texture2D> Image;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}