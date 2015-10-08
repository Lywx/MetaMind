// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewLogic.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Views.Logic
{
    using Entities;
    using Item.Data;
    using Item.Factories;
    using Layouts;
    using Scrolls;
    using Selections;
    using Swaps;

    public interface IMMViewController : IViewLogicOperations, IViewComponent, IMMInputable
    {
        IViewSelectionController ViewSelection { get; }

        IViewScrollController ViewScroll { get; }

        IViewLayout ViewLayout { get; }

        IViewSwapController ViewSwap { get; }

        IViewBinding ViewBinding { get; set; }

        IViewItemFactory ItemFactory { get; }
    }
}