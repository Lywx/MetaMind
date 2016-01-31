// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewSwapController.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Core.Entity.Control.Views.Swaps
{
    using System;
    using Item;
    using Microsoft.Xna.Framework;

    public interface IMMViewSwapController : IMMViewComponent, IDisposable
    {
        #region Observers 

        void AddObserver(IMMView view);

        void RemoveObserver(IMMView view);

        #endregion

        #region Status 

        float Progress { get; set; }

        Vector2 Position { get; }

        #endregion

        #region Process

        void StartProcess(IMMViewItem touchedItem, Vector2 touchedStart, IMMViewItem draggingItem, IMMView draggingView, Vector2 draggingEnd);

        void WatchProcess(IMMViewItem item);

        #endregion
    }
}