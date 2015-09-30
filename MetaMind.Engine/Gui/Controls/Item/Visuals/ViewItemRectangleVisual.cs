// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemFrameVisual.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Gui.Controls.Item.Visuals
{
    using Elements.Rectangles;
    using Images;
    using Microsoft.Xna.Framework;
    using Service;

    public class ViewItemRectangleVisual : ViewItemComponent, IViewItemVisual
    {
        public ViewItemRectangleVisual(IViewItem item, IPressableRectangle rectangle, ViewItemVisualSettings settings)
            : base(item)
        {
            this.Rectangle = rectangle;
            this.Settings  = settings;

            this.Filling = new ImageBox(
                this.Item.GetImageSelector(this.Settings),
                this.Item.GetBoundsSelector(this.Settings, this.Rectangle.Bounds),
                this.Item.GetColorSelector(this.Settings));

            this.Boundary = new ColorBox(
                this.Item.GetBoundsSelector(this.Settings, this.Rectangle.Bounds),
                this.Item.GetColorSelector(this.Settings))
            {
                ColorFilled = false
            };
        }

        #region Dependency

        public ViewItemVisualSettings Settings { get; }

        #endregion

        public ColorBox Boundary { get; }

        public ImageBox Filling { get; }

        public IPressableRectangle Rectangle { get; }

        public override void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!ViewItemState.Item_Is_Active.Match(this.Item))
            {
                return;
            }

            this.Boundary.Draw(graphics, time, alpha);
            this.Filling .Draw(graphics, time, alpha);
        }
    }
}