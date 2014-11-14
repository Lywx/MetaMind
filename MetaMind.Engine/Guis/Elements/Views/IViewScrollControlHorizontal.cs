namespace MetaMind.Engine.Guis.Elements.Views
{
    using Microsoft.Xna.Framework;

    public interface IViewScrollControlHorizontal
    {
        int XOffset { get; }

        bool CanDisplay(int id);

        bool IsLeftToDisplay(int column);

        bool IsRightToDisplay(int column);

        void MoveLeft();

        void MoveRight();

        Point RootCenterPoint(int id);
    }
}