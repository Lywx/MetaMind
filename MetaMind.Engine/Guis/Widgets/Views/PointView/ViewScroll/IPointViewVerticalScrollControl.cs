namespace MetaMind.Engine.Guis.Widgets.Views.PointView.ViewScroll
{
    public interface IPointViewVerticalScrollControl : IPointViewScrollControl
    {
        int OffsetY { get; }

        bool IsDownToDisplay(int row);

        bool IsUpToDisplay(int row);

        void MoveDown();

        void MoveUp();

        void MoveUpToTop();
    }
}