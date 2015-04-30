namespace MetaMind.Engine.Guis.Widgets.Views.PointView.ViewScroll
{
    public interface IPointViewHorizontalScrollControl : IPointViewScrollControl
    {
        int OffsetX { get; }

        bool IsLeftToDisplay(int column);

        bool IsRightToDisplay(int column);

        void MoveLeft();

        void MoveRight();
    }
}