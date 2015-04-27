namespace MetaMind.Engine.Guis.Widgets.Regions
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
            if (this.Frame[FrameState.Mouse_Over]())
            {
                this[RegionState.Region_Mouse_Over] = () => true;
            }
            else
            {
                this[RegionState.Region_Mouse_Over] = () => false;
            }

            if (this.Frame[FrameState.Mouse_Left_Pressed]() ||
                this.Frame[FrameState.Mouse_Left_Double_Clicked]() ||
                this.Frame[FrameState.Mouse_Right_Pressed]() ||
                this.Frame[FrameState.Mouse_Right_Double_Clicked]())
            {
                this[RegionState.Region_Has_Focus] = () => true;
            }
            else
            {
                this[RegionState.Region_Has_Focus] = () => false;
            }
        }
    }
}