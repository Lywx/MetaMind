// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewLogic.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace MetaMind.Engine.Gui.Control.Views.Logic
{
    using System;
    using Component.Input;
    using Item;
    using Item.Data;
    using Item.Factories;
    using Layouts;
    using Microsoft.Xna.Framework;
    using Scrolls;
    using Selections;
    using Service;
    using Swaps;

    public abstract class ViewLogic : ViewComponent, IViewLogic
    {
        protected ViewLogic(
            IView                    view,
            IViewScrollController    viewScroll,
            IViewSelectionController viewSelection,
            IViewSwapController      viewSwap,
            IViewLayout              viewLayout,
            IViewItemFactory itemFactory)
            : this(view, itemFactory)
        {
            if (viewScroll == null)
            {
                throw new ArgumentNullException(nameof(viewScroll));
            }

            if (viewSelection == null)
            {
                throw new ArgumentNullException(nameof(viewSelection));
            }

            if (viewSwap == null)
            {
                throw new ArgumentNullException(nameof(viewSwap));
            }

            if (viewLayout == null)
            {
                throw new ArgumentNullException(nameof(viewLayout));
            }

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
                throw new ArgumentNullException(nameof(itemFactory));
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

        public IViewSelectionController ViewSelection { get; protected set; }

        public IViewScrollController ViewScroll { get; protected set; }

        public IViewSwapController ViewSwap { get; protected set; }

        public IViewLayout ViewLayout { get; protected set; }

        public IViewItemFactory ItemFactory { get; protected set; }

        public IViewBinding ViewBinding { get; set; }

        #region Binding

        public void LoadBinding()
        {
            this.ViewBinding.Bind();

            this.ResetItems();
        }

        public void UnloadBinding()
        {
            this.ViewBinding.Unbind();
        }

        #endregion

        #region Layer

        public override void Initialize()
        {
            base.Initialize();

            // This is order insensitive
            this.ViewLayout   .Initialize();
            this.ViewScroll   .Initialize();
            this.ViewSelection.Initialize();
            this.ViewSwap     .Initialize();
            this.ViewLayout   .Initialize();
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
            base.Update(time);

            foreach (var item in this.View.ItemsRead.ToArray())
            {
                item.UpdateView(time);
                item.Update(time);
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
            if (this.View.ViewSettings.ReadOnly)
            {
                return;
            }

            var item = new ViewItem(this.View, this.View.ItemSettings);

            item.ItemLayer  = this.ItemFactory.CreateLayer(item);
            item.ItemLogic  = this.ItemFactory.CreateLogic(item);
            item.ItemVisual = this.ItemFactory.CreateVisual(item);

            item.ItemData = this.ViewBinding.AddData(item);

            item.Initialize();

            // HACK: Update item frame to avoid flickering
            item.UpdateView(new GameTime());
            item.Update(new GameTime());

            this.View.ItemsWrite.Add(item);
        }

        public void AddItem(dynamic data)
        {
            var item = new ViewItem(this.View, this.View.ItemSettings);

            item.ItemLayer  = this.ItemFactory.CreateLayer(item);
            item.ItemLogic  = this.ItemFactory.CreateLogic(item);
            item.ItemVisual = this.ItemFactory.CreateVisual(item);

            item.ItemData = data;

            item.Initialize();

            // HOTFIX: Update item frame to avoid flickering
            item.UpdateView(new GameTime());
            item.Update(new GameTime());

            this.View.ItemsWrite.Add(item);
        }

        public void ResetItems()
        {
            // Avoid repetitive item adding by calling ResetItems() multiple time
            this.ItemsRead.Clear();

            this.ItemsWrite.Clear();

            // HOTFIX: Has to be after the item clear(in this.ItemsRead) 
            // because it is possible to view scroll control
            // to use the this.ItemsRead information in Reset()
            this.DeferAction(this.ViewScroll.Reset);

            foreach (var data in this.ViewBinding.AllData)
            {
                // Safe threading
                var localData = data;

                this.DeferAction(() => this.AddItem(localData));
            }
        }

        #endregion
    }
}