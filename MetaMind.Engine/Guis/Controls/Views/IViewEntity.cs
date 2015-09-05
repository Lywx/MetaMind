// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewEntity.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    public interface IViewEntity : IGameControllableEntity
    {
        #region States

        bool[] States { get; }

        Func<bool> this[ViewState state] { get; set; }

        #endregion
    }
}