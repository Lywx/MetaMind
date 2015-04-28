namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IPointViewHorizontalScrollControl : IPointViewScrollControl
    {
        int XOffset { get; }

        bool IsLeftToDisplay(int column);

        bool IsRightToDisplay(int column);

        void MoveLeft();

        void MoveRight();
    }
}