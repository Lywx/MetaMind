namespace MetaMind.Engine.Guis.Widgets.Views.PointView.ViewSwap
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;

    public interface IViewSwapControl : ICrossViewSwapObservor, ICrossViewTransitObservor, IDisposable
    {
        #region Observors

        List<IView> Observors { get; }

        void AddObserver(IView view);

        #endregion

        float Progress { get; set; }

        Point RootCenterPosition { get; }

        void StartProcess(Point start, Point end);
    }
}