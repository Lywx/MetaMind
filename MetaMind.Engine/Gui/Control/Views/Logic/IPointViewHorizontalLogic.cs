// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPointViewHorizontalLogic.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Control.Views.Logic
{
    using System;
    using Layouts;
    using Scrolls;
    using Selections;
    using Swaps;

    public interface IPointViewHorizontalLogic : IViewLogic
    {
        new IViewSwapController ViewSwap { get; } 

        new IPointViewHorizontalSelectionController ViewSelection { get; }

        new IPointViewHorizontalScrollController ViewScroll { get; }

        new IPointViewHorizontalLayout ViewLayout { get; }

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