// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewLogic.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Entities.Controls.Views.Controllers
{
    using Entities;
    using Item.Data;
    using Item.Factories;
    using Layouts;
    using Scrolls;
    using Selections;
    using Swaps;

    public interface IMMViewController : IMMViewControllerOperations, IMMViewComponent, IMMInputable
    {
        IMMViewSelectionController ViewSelection { get; }

        IMMViewScrollController ViewScroll { get; }

        IMMViewLayout ViewLayout { get; }

        IMMViewSwapController ViewSwap { get; }

        IMMViewBinding ViewBinding { get; set; }

        IViewItemFactory ItemFactory { get; }
    }
}