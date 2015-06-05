namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    public interface IPointViewHorizontalScrollController : IPointViewScrollController
    {
        int ColumnOffset { get; }

        bool IsLeftToDisplay(int id);

        bool IsRightToDisplay(int id);

        void MoveLeft();

        void MoveRight();
    }
}