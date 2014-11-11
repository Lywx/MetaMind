using MetaMind.Engine.Guis.Elements.Abstract;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Elements.Frames
{
    public interface IPressableFrame : IPressable
    {
        #region Public Properties

        bool[ ] States { get; }

        Point Center { get; set; }

        Point Size { get; set; }

        Point Location { get; set; }
        int X { get; set; }

        int Y { get; set; }

        int Width { get; set; }

        int Height { get; set; }

        Rectangle Rectangle { get; set; }

        #endregion Public Properties

        void UpdateInput( GameTime gameTime );

        bool IsEnabled( FrameState state );
    }
}