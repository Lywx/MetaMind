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
        new IViewSwapController ViewSwap { get; } 

        new IPointViewHorizontalSelectionController ViewSelection { get; }

        new IPointViewHorizontalScrollController ViewScroll { get; }
    }
}