﻿namespace MetaMind.Engine.Gui.Controls.Item.Visuals
{
    using Microsoft.Xna.Framework;
    using Services;

    public abstract class ViewItemVisual : ViewItemComponent, IViewItemVisual
    {
        public ViewItemVisual(IViewItem item)
            : base(item)
        {
        }

        /// <remarks>
        /// Forced reimplementation.
        /// </remarks>>
        public abstract override void Draw(GameTime time);
    }
}