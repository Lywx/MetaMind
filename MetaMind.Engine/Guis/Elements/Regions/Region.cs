﻿using MetaMind.Engine.Guis.Elements.Frames;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Elements.Regions
{
    public class Region : RegionObject, IRegion
    {
        private IPickableFrame frame;

        public Region( Rectangle rectangle )
        {
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
            get { return Frame.Height; }
            set { Frame.Height = value; }
        }

        public Point Location
        {
            get { return frame.Location; } 
            set { frame.Location = value; }
        }
        public Rectangle Rectangle
        {
            set { frame.Rectangle = value; }
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