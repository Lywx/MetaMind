namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IPointGridControl : IPointViewControl2D
    {
        bool Locked { get; }

        ViewRegion Region { get; }

        PointViewScrollBar ScrollBar { get; }

        void ScrollDown();

        void ScrollUp();
    }
}