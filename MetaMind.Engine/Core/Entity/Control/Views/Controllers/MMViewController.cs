namespace MetaMind.Engine.Core.Entity.Control.Views.Controllers
{
    using System;
    using Backend.Input;
    using Item;
    using Item.Data;
    using Item.Factories;
    using Layouts;
    using Microsoft.Xna.Framework;
    using Scrolls;
    using Selections;
    using Swaps;

    public abstract class MMViewController : MMViewControlComponent, IMMViewController
    {
        protected MMViewController(
            IMMView view,
            IMMViewScrollController viewScroll,
            IMMViewSelectionController viewSelection,
            IMMViewSwapController viewSwap,
            IMMViewLayout viewLayout,
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

            this.ViewScroll = viewScroll;
            this.ViewSelection = viewSelection;
            this.ViewSwap = viewSwap;
            this.ViewLayout = viewLayout;
        }

        private MMViewController(IMMView view, IViewItemFactory itemFactory)
            : base(view)
        {
            if (itemFactory == null)
            {
                throw new ArgumentNullException(nameof(itemFactory));
            }

            this.ItemFactory = itemFactory;

            this.View[MMViewState.View_Is_Active] = this.ViewIsActive;
            this.View[MMViewState.View_Is_Inputting] = this.ViewIsInputting;
        }

        public IViewItemFactory ItemFactory { get; protected set; }

        public IMMViewBinding ViewBinding { get; set; }

        public IMMViewLayout ViewLayout { get; protected set; }

        public IMMViewScrollController ViewScroll { get; protected set; }

        public IMMViewSelectionController ViewSelection { get; protected set; }

        public IMMViewSwapController ViewSwap { get; protected set; }

        private Func<bool> ViewIsActive
        {
            get
            {
                return () => true;
            }
        }

        private Func<bool> ViewIsInputting
        {
            get
            {
                return () => this.View[MMViewState.View_Is_Active]() &&
                            !this.View[MMViewState.View_Is_Editing]() &&
                             this.View[MMViewState.View_Has_Focus]();
            }
        }

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
            this.ViewLayout.Initialize();
            this.ViewScroll.Initialize();
            this.ViewSelection.Initialize();
            this.ViewSwap.Initialize();
            this.ViewLayout.Initialize();
        }

        #endregion

        #region Buffer

        public override void UpdateBackwardBuffer()
        {
            base.UpdateBackwardBuffer();

            // This is order insensitive
            this.ViewLayout.UpdateBackwardBuffer();
            this.ViewScroll.UpdateBackwardBuffer();
            this.ViewSelection.UpdateBackwardBuffer();
            this.ViewSwap.UpdateBackwardBuffer();
            this.ViewLayout.UpdateBackwardBuffer();

            foreach (var item in this.Items.ToArray())
            {
                item.UpdateBackwardBuffer();
            }
        }

        public override void UpdateForwardBuffer()
        {
            base.UpdateForwardBuffer();

            // This is order insensitive
            this.ViewLayout.UpdateForwardBuffer();
            this.ViewScroll.UpdateForwardBuffer();
            this.ViewSelection.UpdateForwardBuffer();
            this.ViewSwap.UpdateForwardBuffer();
            this.ViewLayout.UpdateForwardBuffer();

            foreach (var item in this.Items.ToArray())
            {
                item.UpdateForwardBuffer();
            }
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            base.Update(time);

            foreach (var item in this.View.Items.ToArray())
            {
                item.UpdateView(time);
                item.Update(time);
            }
        }

        public override void UpdateInput(GameTime time)
        {
            this.UpdateMouseInput(time);
            this.UpdateKeyboardInput(time);
            this.UpdateItemsInput(time);
        }

        protected void UpdateItemsInput(GameTime time)
        {
            foreach (var item in this.View.Items.ToArray())
            {
                item.UpdateInput(time);
            }
        }

        // TODO: Not immediate mode enough, I may need to process
        //       computationally heavy task in a single run by collecting input
        //       in a structure.
        protected virtual void UpdateKeyboardInput(GameTime time)
        {
            if (this.View[MMViewState.View_Is_Inputting]())
            {
                if (this.View.ViewSettings.KeyboardEnabled)
                {
                    var keyboard = this.GlobalInput.State.Keyboard;
                    if (keyboard.IsActionTriggered(MMInputActions.CommonCreateItem))
                    {
                        this.AddItem();
                    }

                    if (keyboard.IsActionTriggered(MMInputActions.Escape))
                    {
                        this.ViewSelection.Cancel();
                    }
                }
            }
        }

        protected virtual void UpdateMouseInput(GameTime time)
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

            var item = new MMViewItem(this.View, this.View.ItemSettings);

            item.ItemLayer = this.ItemFactory.CreateLayer(item);
            item.ItemLogic = this.ItemFactory.CreateController(item);
            item.Renderer = this.ItemFactory.CreateRenderer(item);

            item.ItemData = this.ViewBinding.AddData(item);

            item.Initialize();

            // HACK: Update item frame to avoid flickering
            item.UpdateView(new GameTime());
            item.Update(new GameTime());

            this.ItemsWrite.Add(item);
        }

        public void AddItem(dynamic data)
        {
            var item = new MMViewItem(this.View, this.View.ItemSettings);

            item.ItemLayer = this.ItemFactory.CreateLayer(item);
            item.ItemLogic = this.ItemFactory.CreateController(item);
            item.Renderer = this.ItemFactory.CreateRenderer(item);

            item.ItemData = data;

            item.Initialize();

            // HOTFIX: Update item frame to avoid flickering
            item.UpdateView(new GameTime());
            item.Update(new GameTime());

            this.ItemsWrite.Add(item);
        }

        public void ResetItems()
        {
            // Avoid repetitive item adding by calling ResetItems() multiple time
            this.Items.Clear();

            this.ItemsWrite.Clear();

            // HOTFIX: Has to be after the item clear(in this.ItemsRead)
            // because it is possible to view scroll control
            // to use the this.ItemsRead information in Reset()
            this.CacheAction(this.ViewScroll.Reset);

            foreach (var data in this.ViewBinding.AllData)
            {
                // Safe threading
                var localData = data;

                this.CacheAction(() => this.AddItem(localData));
            }
        }

        #endregion
    }
}