using MetaMind.Engine.Guis.Elements.Frames;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Elements.Regions
{
    public class Region : RegionObject, IRegion
    {
        private IPickableFrame frame;

        private int height;
        private int width;
        private int x;
        private int y;

        public Region( Rectangle rectangle )
        {
            x = rectangle.X;
            y = rectangle.Y;
            width = rectangle.Width;
            height = rectangle.Height;

            frame = new PickableFrame( rectangle );
        }

        public Region( int x, int y, int width, int height ) :
            this( new Rectangle( x, y, width, height ) )
        {
        }

        public IPickableFrame Frame
        {
            get { return frame; }
            set { frame = value; }
        }
        public int Height
        {
            get { return height; }
            set
            {
                height = value;
                Frame.Rectangle = new Rectangle( x, y, width, height );
            }
        }
        public Rectangle Rectangle
        {
            set { frame.Rectangle = value; }
        }
        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                Frame.Rectangle = new Rectangle( x, y, width, height );
            }
        }

        public int X
        {
            get { return x; }
            set
            {
                x = value;
                Frame.Rectangle = new Rectangle( x, y, width, height );
            }
        }

        public int Y
        {
            get { return y; }
            set
            {
                y = value;
                Frame.Rectangle = new Rectangle( x, y, width, height );
            }
        }

        public virtual void Update( GameTime gameTime )
        {
            if ( frame.IsEnabled( FrameState.Mouse_Left_Clicked ) || frame.IsEnabled( FrameState.Mouse_Left_Double_Clicked ) ||
                frame.IsEnabled( FrameState.Mouse_Right_Clicked ) || frame.IsEnabled( FrameState.Mouse_Right_Double_Clicked ) )
                Enable( RegionState.Region_Hightlighted );
            else
                Disable( RegionState.Region_Hightlighted );
        }
    }
}