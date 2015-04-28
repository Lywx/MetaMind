namespace MetaMind.Engine.Guis.Widgets.Views.PointView
{
    public interface IPointGridLogicControl : IPointView2DLogicControl
    {
        bool Locked { get; }

        ViewRegion Region { get; }

        ViewVerticalScrollBar Scrollbar { get; }

        void ScrollDown();

        void ScrollUp();
    }
}