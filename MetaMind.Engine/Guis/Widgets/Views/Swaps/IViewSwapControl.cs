// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewSwapControl.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.Swaps
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Items.Layouts;
    using MetaMind.Engine.Guis.Widgets.Items.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public interface IViewSwapControl : IDisposable
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

        void StartProcess(
            IGameInteropService interop,
            IViewItem touchedItem,
            IViewItem draggingItem,
            IViewLogic draggingViewLogic);

        void WatchProcess(IViewItem item);

        #endregion
    }
}