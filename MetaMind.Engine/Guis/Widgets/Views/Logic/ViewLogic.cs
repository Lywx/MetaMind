// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewLogic.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using System;
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.Items.Factories;
    using MetaMind.Engine.Guis.Widgets.Views.Layouts;
    using MetaMind.Engine.Guis.Widgets.Views.Scrolls;
    using MetaMind.Engine.Guis.Widgets.Views.Selections;
    using MetaMind.Engine.Guis.Widgets.Views.Swaps;

    public abstract class ViewLogic<TData> : ViewComponent, IViewLogic
    {
        protected ViewLogic(
            IView                    view, 
            IList<TData>             viewData, 
            IViewScrollController    viewScroll,
            IViewSelectionController viewSelection, 
            IViewSwapController      viewSwap, 
            IViewLayout              viewLayout,
            IViewItemFactory itemFactory)
            : this(view, itemFactory)
        {
            if (viewData == null)
            {
                throw new ArgumentNullException("viewData");
            }

            if (viewScroll == null)
            {
                throw new ArgumentNullException("viewScroll");
            }

            if (viewSelection == null)
            {
                throw new ArgumentNullException("viewSelection");
            }

            if (viewSwap == null)
            {
                throw new ArgumentNullException("viewSwap");
            }

            if (viewLayout == null)
            {
                throw new ArgumentNullException("viewLayout");
            }

            this.ViewData      = viewData;
            this.ViewScroll    = viewScroll;
            this.ViewSelection = viewSelection;
            this.ViewSwap      = viewSwap;
            this.ViewLayout    = viewLayout;
        }

        private ViewLogic(IView view, IViewItemFactory itemFactory)
            : base(view)
        {
            if (itemFactory == null)
            {
                throw new ArgumentNullException("itemFactory");
            }

            this.ItemFactory = itemFactory;

            this.View[ViewState.View_Is_Active]    = this.ViewIsActive;
            this.View[ViewState.View_Is_Inputting] = this.ViewIsInputting;
        }

        private Func<bool> ViewIsInputting
        {
            get
            {
                return () => this.View[ViewState.View_Is_Active]() && 
                            !this.View[ViewState.View_Is_Editing]() && 
                             this.View[ViewState.View_Has_Focus]();
            }
        }

        private Func<bool> ViewIsActive
        {
            get
            {
                return () => true;
            }
        }

        public IList<TData> ViewData { get; private set; }

        public IViewSelectionController ViewSelection { get; protected set; }

        public IViewScrollController ViewScroll { get; protected set; }

        public IViewSwapController ViewSwap { get; protected set; }

        public IViewLayout ViewLayout { get; protected set; }

        public IViewItemFactory ItemFactory { get; protected set; }
    }
}