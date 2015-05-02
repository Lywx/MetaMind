namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using MetaMind.Engine.Guis.Widgets.Views.Visuals;

    public interface IPointGridLogic : IPointView2DLogic
    {
        bool Locked { get; }

        ViewRegion Region { get; }

        ViewVerticalScrollBar Scrollbar { get; }

        void ScrollDown();

        void ScrollUp();
    }
}