namespace MetaMind.Engine.Guis.Widgets.Views.PointView
{
    public interface IPointViewVerticalScrollControl : IPointViewScrollControl
    {
        int YOffset { get; }

        bool IsDownToDisplay(int row);

        bool IsUpToDisplay(int row);

        void MoveDown();

        void MoveUp();

        void MoveUpToTop();
    }
}