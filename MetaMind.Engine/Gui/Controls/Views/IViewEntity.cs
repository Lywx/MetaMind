// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewEntity.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Views
{
    using System;

    public interface IViewEntity : IGameInputableEntity
    {
        #region States

        Func<bool> this[ViewState state] { get; set; }

        #endregion
    }
}