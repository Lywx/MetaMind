namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IPointGridLogicControl : IPointView2DLogicControl
    {
        bool Locked { get; }

        ViewRegion Region { get; }

        ViewVerticalScrollBar ScrollBar { get; }

        void ScrollDown();

        void ScrollUp();
    }
}