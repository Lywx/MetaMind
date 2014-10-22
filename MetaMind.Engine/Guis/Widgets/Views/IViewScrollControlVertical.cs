using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IViewScrollControlVertical
    {
        int YOffset { get; }

        bool CanDisplay( int id );

        bool IsDownToDisplay( int row );

        bool IsUpToDisplay( int row );

        void MoveDown();

        void MoveUp();

        Point RootCenterPoint( int id );
    }
}