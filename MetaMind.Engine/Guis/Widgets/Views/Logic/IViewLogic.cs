// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewLogic.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using Items.Data;
    using MetaMind.Engine.Guis.Widgets.Items.Factories;
    using MetaMind.Engine.Guis.Widgets.Views.Layouts;
    using MetaMind.Engine.Guis.Widgets.Views.Scrolls;
    using MetaMind.Engine.Guis.Widgets.Views.Selections;
    using MetaMind.Engine.Guis.Widgets.Views.Swaps;

    public interface IViewLogic : IViewLogicOperations, IViewComponent, IInputable
    {
        IViewSelectionController ViewSelection { get; }

        IViewScrollController ViewScroll { get; }

        IViewSwapController ViewSwap { get; }

        IViewLayout ViewLayout { get; }

        IViewItemFactory ItemFactory { get; }

        IViewItemBinding ItemBinding { get; }
    }
}