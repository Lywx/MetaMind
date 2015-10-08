// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewSwapController.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Views.Swaps
{
    using System;
    using Item;
    using Microsoft.Xna.Framework;
    using Services;

    public interface IViewSwapController : IViewComponent, IDisposable
    {
        #region Observers 

        void AddObserver(IMMViewNode view);

        void RemoveObserver(IMMViewNode view);

        #endregion

        #region Status 

        float Progress { get; set; }

        Vector2 Position { get; }

        #endregion

        #region Process

        void StartProcess(IMMEngineInteropService interop, IViewItem touchedItem, Vector2 touchedStart, IViewItem draggingItem, IMMViewNode draggingView, Vector2 draggingEnd);

        void WatchProcess(IViewItem item);

        #endregion
    }
}