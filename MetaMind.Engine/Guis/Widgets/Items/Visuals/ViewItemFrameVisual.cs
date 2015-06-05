namespace MetaMind.Engine.Guis.Widgets.Items.Visuals
{
    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Guis.Widgets.Items.Frames;
    using MetaMind.Engine.Guis.Widgets.Visuals;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;
    using Testers;

    public class ViewItemFrameVisual : ViewItemComponent, IViewItemVisual
    {
        private readonly IPressableFrame frame;

        private readonly FrameSettings frameSettings;

        private readonly Box frameBoxFilled;

        private readonly Box frameBoxDrawn;

        public ViewItemFrameVisual(IViewItem item, IPressableFrame frame, FrameSettings frameSettings)
            : base(item)
        {
            this.frame         = frame;
            this.frameSettings = frameSettings;
            
            this.frameBoxFilled = new Box(
                () => this.frame.Rectangle.Crop(this.frameSettings.Margin),
                () =>
                    {
                        if (this.Item[ItemState.Item_Is_Pending]())
                        {
                            return this.frameSettings.PendingColor;
                        }

                        if (this.Item[ItemState.Item_Is_Editing]())
                        {
                            return this.frameSettings.ModificationColor;
                        }

                        if (this.Item[ItemState.Item_Is_Mouse_Over]() && 
                            this.Item[ItemState.Item_Is_Selected]())
                        {
                            return this.frameSettings.SelectionColor;
                        }

                        if (this.Item[ItemState.Item_Is_Mouse_Over]() && 
                           !this.Item[ItemState.Item_Is_Selected]())
                        {
                            return this.frameSettings.RegularColor;
                        }

                        if (this.Item[ItemState.Item_Is_Selected]())
                        {
                            return this.frameSettings.SelectionColor;
                        }

                        return this.frameSettings.RegularColor;
                    },
                () => true);

            this.frameBoxDrawn = new Box(
                () => this.frame.Rectangle.Crop(this.frameSettings.Margin),
                () =>
                    {
                        if (this.Item[ItemState.Item_Is_Mouse_Over]() && 
                            this.Item[ItemState.Item_Is_Editing]())
                        {
                            return this.frameSettings.MouseOverColor;
                        }
                        
                        if (this.Item[ItemState.Item_Is_Mouse_Over]() && 
                            this.Item[ItemState.Item_Is_Selected]())
                        {
                            return this.frameSettings.MouseOverColor;
                        }
                        
                        if (this.Item[ItemState.Item_Is_Mouse_Over]() && 
                           !this.Item[ItemState.Item_Is_Selected]())
                        {
                            return this.frameSettings.MouseOverColor;
                        }

                        return Color.Transparent;
                    },
                () => false);
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!this.Item[ItemState.Item_Is_Active]() && 
                !this.Item[ItemState.Item_Is_Dragging]())
            {
                return;
            }

            this.frameBoxDrawn .Draw(graphics, time, alpha);
            this.frameBoxFilled.Draw(graphics, time, alpha);
        }
    }
}