// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPointViewHorizontalLogic.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Core.Entity.Control.Views.Controllers
{
    using System;
    using Layouts;
    using Scrolls;
    using Selections;
    using Swaps;

    public interface IMMPointViewHorizontalController : IMMViewController
    {
        new IMMViewSwapController ViewSwap { get; } 

        new IMMPointViewHorizontalSelectionController ViewSelection { get; }

        new IMMPointViewHorizontalScrollController ViewScroll { get; }

        new IMMPointViewHorizontalLayout ViewLayout { get; }

        #region Events

        event EventHandler ScrolledLeft;

        event EventHandler ScrolledRight;

        event EventHandler MovedLeft;

        event EventHandler MovedRight;

        #endregion

        #region Operations

        void MoveLeft();

        void FastMoveLeft();

        void MoveRight();

        void FastMoveRight();

        #endregion
    }
}