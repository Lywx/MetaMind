namespace MetaMind.Engine.Guis.Elements.Views
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

        Point RootCenterPoint(int id);
    }
}