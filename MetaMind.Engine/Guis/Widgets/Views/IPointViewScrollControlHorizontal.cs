namespace MetaMind.Engine.Guis.Widgets.Views
{
    using Microsoft.Xna.Framework;

    public interface IPointViewScrollControlHorizontal
    {
        int XOffset { get; }

        bool CanDisplay(int id);

        bool IsLeftToDisplay(int column);

        bool IsRightToDisplay(int column);

        void MoveLeft();

        void MoveRight();

        Point RootCenterPoint(int id);

        void Zoom(int id);
    }
}