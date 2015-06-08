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
    using Components.Inputs;
    using Items;
    using Items.Data;
    using MetaMind.Engine.Guis.Widgets.Items.Factories;
    using MetaMind.Engine.Guis.Widgets.Views.Layouts;
    using MetaMind.Engine.Guis.Widgets.Views.Scrolls;
    using MetaMind.Engine.Guis.Widgets.Views.Selections;
    using MetaMind.Engine.Guis.Widgets.Views.Swaps;
    using Microsoft.Xna.Framework;
    using Services;

    public abstract class ViewLogic<TData> : ViewComponent, IViewLogic 
    {
        protected ViewLogic(
            IView                    view,
            IList<TData>             viewData,
            IViewScrollController    viewScroll,
            IViewSelectionController viewSelection,
            IViewSwapController<TData> viewSwap,
            IViewLayout              viewLayout,
            IViewItemBinding<TData> itemBinding,
            IViewItemFactory itemFactory)
            : this(view, itemBinding, itemFactory)
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

        private ViewLogic(IView view, IViewItemBinding itemBinding, IViewItemFactory itemFactory)
            : base(view)
        {
            if (itemBinding == null)
            {
                throw new ArgumentNullException("itemBinding");
            }

            if (itemFactory == null)
            {
                throw new ArgumentNullException("itemFactory");
            }

            this.ItemBinding = itemBinding;
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

        public IViewItemBinding ItemBinding { get; private set; }

        #region Binding

        public void SetupBinding()
        {
            foreach (var data in this.ItemBinding.AllData())
            {
                this.AddItem(data);
            }
        }

        #endregion

        #region Layer

        public override void SetupLayer()
        {
            base.SetupLayer();

            // This is order insensitive
            this.ViewLayout   .SetupLayer();
            this.ViewScroll   .SetupLayer();
            this.ViewSelection.SetupLayer();
            this.ViewSwap     .SetupLayer();
            this.ViewLayout   .SetupLayer();
        }

        #endregion

        #region Buffer

        public override void UpdateBackwardBuffer()
        {
            base.UpdateBackwardBuffer();

            // This is order insensitive
            this.ViewLayout   .UpdateBackwardBuffer();
            this.ViewScroll   .UpdateBackwardBuffer();
            this.ViewSelection.UpdateBackwardBuffer();
            this.ViewSwap     .UpdateBackwardBuffer();
            this.ViewLayout   .UpdateBackwardBuffer();

            foreach (var item in this.ItemsRead.ToArray())
            {
                item.UpdateBackwardBuffer();
            }
        }

        public override void UpdateForwardBuffer()
        {
            base.UpdateForwardBuffer();

            // This is order insensitive
            this.ViewLayout   .UpdateForwardBuffer();
            this.ViewScroll   .UpdateForwardBuffer();
            this.ViewSelection.UpdateForwardBuffer();
            this.ViewSwap     .UpdateForwardBuffer();
            this.ViewLayout   .UpdateForwardBuffer();

            foreach (var item in this.ItemsRead.ToArray())
            {
                item.UpdateForwardBuffer();
            }
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            if (this.View[ViewState.View_Is_Active]())
            {
                foreach (var item in this.View.ItemsRead.ToArray())
                {
                    item.UpdateView(time);
                    item.Update(time);
                }
            }
            else
            {
                foreach (var item in this.View.ItemsRead.ToArray())
                {
                    item.UpdateView(time);
                }
            }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.UpdateInputOfMouse(input, time);
            this.UpdateInputOfKeyboard(input, time);
            this.UpdateInputOfItems(input, time);
        }

        protected void UpdateInputOfItems(IGameInputService input, GameTime time)
        {
            foreach (var item in this.View.ItemsRead.ToArray())
            {
                item.UpdateInput(input, time);
            }
        }

        protected virtual void UpdateInputOfKeyboard(IGameInputService input, GameTime time)
        {
            if (this.View[ViewState.View_Is_Inputting]())
            {
                if (this.View.ViewSettings.KeyboardEnabled)
                {
                    var keyboard = input.State.Keyboard;
                    if (keyboard.IsActionTriggered(KeyboardActions.CommonCreateItem))
                    {
                        this.AddItem();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.Escape))
                    {
                        this.ViewSelection.Cancel();
                    }
                }
            }
        }

        protected virtual void UpdateInputOfMouse(IGameInputService input, GameTime time)
        {
        }

        #endregion

        #region Operations

        public void AddItem()
        {
            if (!this.View.ViewSettings.ReadOnly)
            {
                var item = new ViewItem(this.View, this.View.ItemSettings);

                item.ItemLayer  = this.ItemFactory.CreateLayer(item);
                item.ItemLogic  = this.ItemFactory.CreateLogic(item);
                item.ItemVisual = this.ItemFactory.CreateVisual(item);

                item.ItemData = this.ItemBinding.AddData(item);

                item.SetupLayer();

                this.View.ItemsWrite.Add(item);
            }
        }

        public void AddItem(dynamic data)
        {
            var item = new ViewItem(this.View, this.View.ItemSettings);

            item.ItemLayer  = this.ItemFactory.CreateLayer(item);
            item.ItemLogic  = this.ItemFactory.CreateLogic(item);
            item.ItemVisual = this.ItemFactory.CreateVisual(item);

            item.ItemData = data;

            item.SetupLayer();

            this.View.ItemsWrite.Add(item);
        }

        #endregion
    }
}