// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewLogic.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Core.Entity.Control.Views.Controllers
{
    using Entity.Common;
    using Item.Data;
    using Item.Factories;
    using Layouts;
    using Scrolls;
    using Selections;
    using Swaps;

    public interface IMMViewController : IMMViewControllerOperations, IMMViewComponent, IMMInputtable
    {
        IMMViewSelectionController ViewSelection { get; }

        IMMViewScrollController ViewScroll { get; }

        IMMViewLayout ViewLayout { get; }

        IMMViewSwapController ViewSwap { get; }

        IMMViewBinding ViewBinding { get; set; }

        IViewItemFactory ItemFactory { get; }
    }
}