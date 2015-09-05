// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewSwapController.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.Swaps
{
    using System;
    using System.Collections.Generic;
    using Items;
    using Services;

    using Microsoft.Xna.Framework;

    public interface IViewSwapController : IViewComponent, IDisposable
    {
        #region Observers 

        void AddObserver(IView view);

        void RemoveObserver(IView view);

        #endregion

        #region Status 

        float Progress { get; set; }

        Vector2 Position { get; }

        #endregion

        #region Process

        void StartProcess(IGameInteropService interop, IViewItem touchedItem, Vector2 touchedStart, IViewItem draggingItem, IView draggingView, Vector2 draggingEnd);

        void WatchProcess(IViewItem item);

        #endregion
    }
}