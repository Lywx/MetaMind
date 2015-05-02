namespace MetaMind.Engine.Guis.Widgets.Items.Visuals
{
    using System;
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.Items.Frames;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewItemVisual : ViewItemComponent, IViewItemVisual
    {
        public ViewItemVisual(IViewItem item, IItemRootFrame root, Dictionary<string, object> elems)
            : base(item)
        {
            this.IdCenterPosition   = () => root.Center.ToVector2();
            this.ItemCenterPosition = () => root.Center.ToVector2();
        }

        protected Func<Vector2> IdCenterPosition { get; set; }

        protected Func<Vector2> ItemCenterPosition { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!this.Item[ItemState.Item_Is_Active]() && 
                !this.Item[ItemState.Item_Is_Dragging]())
            {
                return;
            }
        }
    }
}