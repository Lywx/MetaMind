// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewItemInteraction.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items.Interactions
{
    using System;

    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    /// <summary>
    ///     Item interaction with view, other items.
    /// </summary>
    public interface IViewItemInteraction : IViewItemComponent, IUpdateable, IDisposable
    {
        /// <summary>
        /// Customized action for item which would be triggered after selection.
        /// </summary>
        void ItemSelect();

        /// <summary>
        /// Customized action for view which would be triggered after selection.
        /// </summary>
        void ViewSelect();
        
        /// <summary>
        /// Customized action for view which would be triggered after unselection.
        /// </summary>
        void ViewUnselect();

        void ViewSwap(IGameInteropService interop, IViewItem draggingItem);
    }
}