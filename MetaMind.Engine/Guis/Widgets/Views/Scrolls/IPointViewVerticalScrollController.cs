namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    public interface IPointViewVerticalScrollController : IPointViewScrollController
    {
        int RowOffset { get; }

        bool IsDownToDisplay(int id);

        bool IsUpToDisplay(int id);

        void MoveDown();

        void MoveUp();

        void MoveUpToTop();
    }
}