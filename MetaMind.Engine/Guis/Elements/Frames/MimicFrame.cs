using MetaMind.Engine.Extensions;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Elements.Frames
{
    public class MimicFrame : PickableFrame
    {
        public MimicFrame()
        {
            
        }

        public void Mimic( IPressableFrame frame )
        {
            Initialize( frame.Rectangle.Center, new Point( frame.Rectangle.Width, frame.Rectangle.Height ) );
        }
        public void Mimic( IPressableFrame frame, Point size )
        {
            Initialize( frame.Rectangle.Center, size );
        }

        public void MimicBottom( IPressableFrame frame, Point size )
        {
            Initialize( ( frame.Rectangle.Center.ToVector2() - new Vector2( 0, size.Y - frame.Rectangle.Height ) ).ToPoint(), size );
        }
    }
}