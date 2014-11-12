using MetaMind.Engine.Guis.Elements.Frames;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Elements.Regions
{
    public class Region : RegionObject, IRegion
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
            get { return Frame.Height; }
            set { Frame.Height = value; }
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
            get { return Frame.Width; }
            set { Frame.Width = value; }
        }

        public int X
        {
            get { return Frame.X; }
            set { Frame.X = value; }
        }

        public int Y
        {
            get { return Frame.Y; }
            set { Frame.Y = value; }
        }

        public virtual void UpdateInput(GameTime gameTime)
        {
            if (this.Frame.IsEnabled(FrameState.Mouse_Left_Clicked) ||
                this.Frame.IsEnabled(FrameState.Mouse_Left_Double_Clicked) ||
                this.Frame.IsEnabled(FrameState.Mouse_Right_Clicked) ||
                this.Frame.IsEnabled(FrameState.Mouse_Right_Double_Clicked))
            {
                Enable(RegionState.Region_Hightlighted);
            }
            else
            {
                Disable(RegionState.Region_Hightlighted);
            }
        }

        public virtual void UpdateStructure(GameTime gameTime)
        {
        }
    }
}