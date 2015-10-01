// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewEntity.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Views
{
    using System;
    using Entities;

    public interface IViewEntity : IMMInputableEntity
    {
        #region States

        Func<bool> this[ViewState state] { get; set; }

        #endregion
    }
}