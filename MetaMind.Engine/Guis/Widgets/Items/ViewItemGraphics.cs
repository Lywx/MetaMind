namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;
    using System.Globalization;

    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Guis.Widgets.Visual;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewItemGraphics : ViewItemComponent, IItemGraphics
    {
        public ViewItemGraphics(IViewItem item)
            : base(item)
        {
            this.RootFrame = this.ItemControl.RootFrame;
            this.NameFrame = this.ItemControl.NameFrame;

            this.IdCenterPosition =   () => this.RootFrame.Center.ToVector2();
            this.ItemCenterPosition = () => this.RootFrame.Center.ToVector2();

            this.IdLabel = new Label(
                () => this.ItemSettings.IdFont,
                () => this.ItemControl.Id.ToString(new CultureInfo("en-US")),
                () => this.IdCenterPosition(),
                () => this.ItemSettings.IdColor,
                () => this.ItemSettings.IdSize,
                StringHAlign.Center,
                StringVAlign.Center,
                false);

            this.NameFilledBox = new Box(
                () => this.NameFrame.Rectangle.Crop((Point)this.ItemSettings.NameFrameMargin),
                () =>
                    {
                        if (this.Item.IsEnabled(ItemState.Item_Pending))
                        {
                            return this.ItemSettings.NameFramePendingColor;
                        }

                        if (this.Item.IsEnabled(ItemState.Item_Editing))
                        {
                            return this.ItemSettings.NameFrameModificationColor;
                        }

                        if (this.Item.IsEnabled(ItemState.Item_Mouse_Over) && 
                            this.Item.IsEnabled(ItemState.Item_Selected))
                        {
                            return this.ItemSettings.NameFrameSelectionColor;
                        }

                        if (this.Item.IsEnabled(ItemState.Item_Mouse_Over) && 
                           !this.Item.IsEnabled(ItemState.Item_Selected))
                        {
                            return this.ItemSettings.NameFrameRegularColor;
                        }

                        if (this.Item.IsEnabled(ItemState.Item_Selected))
                        {
                            return this.ItemSettings.NameFrameSelectionColor;
                        }

                        return this.ItemSettings.NameFrameRegularColor;
                    },
                () => true);

            this.NameDrawnBox = new Box(
                () => this.NameFrame.Rectangle.Crop((Point)this.ItemSettings.NameFrameMargin),
                () =>
                    {
                        if (this.Item.IsEnabled(ItemState.Item_Mouse_Over) && 
                            this.Item.IsEnabled(ItemState.Item_Editing))
                        {
                            return this.ItemSettings.NameFrameMouseOverColor;
                        }
                        
                        if (this.Item.IsEnabled(ItemState.Item_Mouse_Over) && 
                            this.Item.IsEnabled(ItemState.Item_Selected))
                        {
                            return this.ItemSettings.NameFrameMouseOverColor;
                        }
                        
                        if (this.Item.IsEnabled(ItemState.Item_Mouse_Over) && 
                           !this.Item.IsEnabled(ItemState.Item_Selected))
                        {
                            return this.ItemSettings.NameFrameMouseOverColor;
                        }

                        return Color.Transparent;
                    },
                () => false);
        }

        protected IPickableFrame NameFrame { get; set; }

        protected Box NameFilledBox { get; set; }

        protected Box NameDrawnBox { get; set; }

        protected Func<Vector2> IdCenterPosition { get; set; }

        protected Label IdLabel { get; set; }

        protected Func<Vector2> ItemCenterPosition { get; set; }

        protected IItemRootFrame RootFrame { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!ItemControl.Active && 
                !Item.IsEnabled(ItemState.Item_Dragging))
            {
                return;
            }

            this.NameDrawnBox .Draw(graphics, time, alpha);
            this.NameFilledBox.Draw(graphics, time, alpha);

            this.IdLabel.Draw(graphics, time, alpha);
        }
    }
}