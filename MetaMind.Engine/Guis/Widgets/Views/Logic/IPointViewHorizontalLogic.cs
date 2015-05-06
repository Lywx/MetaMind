// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPointViewHorizontalLogic.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using MetaMind.Engine.Guis.Widgets.Views.Scrolls;
    using MetaMind.Engine.Guis.Widgets.Views.Selections;
    using MetaMind.Engine.Guis.Widgets.Views.Swaps;

    public interface IPointViewHorizontalLogic : IViewLogic
    {
        new IPointViewHorizontalSwapControl ViewSwap { get; } 

        new IPointViewHorizontalSelectionControl ViewSelection { get; }

        new IPointViewHorizontalScrollControl ViewScroll { get; }

        #region Item Operations 

        void AddItem();

        #endregion

        #region Movement Operations 

        void FastMoveLeft();

        void FastMoveRight();

        void MoveLeft();

        void MoveRight();

        #endregion
    }
}