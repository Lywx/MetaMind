namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public interface IPointViewSwapControl
    {
        List<IView> Observors { get; }

        float Progress { get; set; }

        void AddObserver(IView view);

        void Initialize(Point origin, Point target);

        Point RootCenterPoint();

        void WatchExchangeIn(IViewItem draggingItem, IView targetView);

        void WatchSwapFrom(IViewItem draggingItem, IView targetView);
    }
}