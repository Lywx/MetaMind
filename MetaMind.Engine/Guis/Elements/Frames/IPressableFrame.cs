using System.Linq.Expressions;
using MetaMind.Engine.Guis.Elements.Abstract;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Elements.Frames
{
    public interface IPressableFrame : IPressable
    {
        bool[ ] States { get; }
        Rectangle Rectangle { get; set; }

        void Update( GameTime gameTime );

        bool IsEnabled( FrameState state );

        void Initialize( Rectangle rectangle );

        void Initialize( Point center, Point size );
    }
}