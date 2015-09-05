namespace MetaMind.Engine.Guis.Controls.Items.Visuals
{
    using Controls.Visuals;
    using Elements;
    using Frames;
    using Microsoft.Xna.Framework;
    using Services;

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
                () => this.Frame.Rectangle.Crop(this.frameSettings.Margin),
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
                () => this.Frame.Rectangle.Crop(this.frameSettings.Margin),
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

        protected Box FrameBoxDrawn
        {
            get { return this.frameBoxDrawn; }
        }

        protected Box FrameBoxFilled
        {
            get { return this.frameBoxFilled; }
        }

        protected IPressableFrame Frame
        {
            get { return this.frame; }
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!this.Item[ItemState.Item_Is_Active]() && 
                !this.Item[ItemState.Item_Is_Dragging]())
            {
                return;
            }

            this.FrameBoxDrawn .Draw(graphics, time, alpha);
            this.FrameBoxFilled.Draw(graphics, time, alpha);
        }
    }
}