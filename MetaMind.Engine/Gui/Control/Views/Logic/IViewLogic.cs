// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewLogic.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Control.Views.Logic
{
    using Item.Data;
    using Item.Factories;
    using Layouts;
    using Scrolls;
    using Selections;
    using Swaps;

    public interface IViewLogic : IViewLogicOperations, IViewComponent, IInputable
    {
        IViewSelectionController ViewSelection { get; }

        IViewScrollController ViewScroll { get; }

        IViewLayout ViewLayout { get; }

        IViewSwapController ViewSwap { get; }

        IViewBinding ViewBinding { get; set; }

        IViewItemFactory ItemFactory { get; }
    }
}