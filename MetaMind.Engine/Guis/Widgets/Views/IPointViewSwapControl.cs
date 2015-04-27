namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public interface IPointViewSwapControl : IDisposable
    {
        #region Observors

        List<IView> Observors { get; }

        void AddObserver(IView view);

        #endregion

        float Progress { get; set; }

        void Initialize(Point origin, Point target);

        Point RootCenterPoint();

        void WatchExchangeIn(IViewItem draggingItem, IView targetView);

        void WatchSwapFrom(IViewItem draggingItem, IView targetView);
    }
}