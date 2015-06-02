// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewSwapController.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.Swaps
{
    using System;
    using Items;
    using Services;

    using Microsoft.Xna.Framework;

    public interface IViewSwapController : IDisposable
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

        void StartProcess(IGameInteropService interop, IViewItem touchedItem, IViewItem draggingItem, IView draggingView);

        void WatchProcess(IViewItem item);

        #endregion
    }
}