namespace MetaMind.Engine.Guis.Widgets.Views
{
    using Microsoft.Xna.Framework;

    public interface IViewScrollControlVertical
    {
        int YOffset { get; }

        bool CanDisplay(int id);

        bool IsDownToDisplay(int row);

        bool IsUpToDisplay(int row);

        void MoveDown();

        void MoveUp();

        void MoveUpToTop();

        Point RootCenterPoint(int id);

        void Zoom(int id);
    }
}