// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewItem.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public interface IViewItem : IItemEntity, IDisposable
    {
        #region Item Data

        dynamic ItemData { get; set; }

        dynamic ItemLogic { get; set; }
        
        IItemVisualControl ItemVisual { get; set; }

        #endregion

        #region View Data

        IView View { get; }

        dynamic ViewLogic { get; }

        dynamic ViewSettings { get; }

        #endregion

        #region Update

        /// <summary>
        /// Update logic related with IView.
        /// </summary>
        /// <param name="time"></param>
        void UpdateView(GameTime time);

        #endregion
    }
}