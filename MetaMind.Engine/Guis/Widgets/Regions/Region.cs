﻿namespace MetaMind.Engine.Guis.Widgets.Regions
{
    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class Region : RegionEntity, IRegion
    {
        public Region(Rectangle rectangle)
        {
            this.Frame = new PickableFrame(rectangle);
        }

        public Region(int x, int y, int width, int height) :
            this(new Rectangle(x, y, width, height))
        {
        }

        public IPickableFrame Frame { get; set; }

        public int Height
        {
            get { return this.Frame.Height; }
            set { this.Frame.Height = value; }
        }

        public Point Location
        {
            get { return this.Frame.Location; }
            set { this.Frame.Location = value; }
        }

        public Rectangle Rectangle
        {
            get { return this.Frame.Rectangle; }
            set { this.Frame.Rectangle = value; }
        }

        public int Width
        {
            get { return this.Frame.Width; }
            set { this.Frame.Width = value; }
        }

        public int X
        {
            get { return this.Frame.X; }
            set { this.Frame.X = value; }
        }

        public int Y
        {
            get { return this.Frame.Y; }
            set { this.Frame.Y = value; }
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (this.Frame.IsEnabled(FrameState.Mouse_Over))
            {
                this.Enable(RegionState.Region_Mouse_Over);
            }
            else
            {
                this.Disable(RegionState.Region_Mouse_Over);
            }

            if (this.Frame.IsEnabled(FrameState.Mouse_Left_Clicked) ||
                this.Frame.IsEnabled(FrameState.Mouse_Left_Clicked_Double) ||
                this.Frame.IsEnabled(FrameState.Mouse_Right_Clicked) ||
                this.Frame.IsEnabled(FrameState.Mouse_Right_Clicked_Double))
            {
                this.Enable(RegionState.Region_Has_Focus);
            }
            else
            {
                this.Disable(RegionState.Region_Has_Focus);
            }
        }
    }
}