﻿namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    using System;
    using Components.Inputs;
    using Data;
    using Frames;
    using Interactions;
    using Layouts;
    using Services;

    using Microsoft.Xna.Framework;
    using Views;

    public class ViewItemLogic : ViewItemComponent, IViewItemLogic
    {
        private bool isFrameUpdated;

        private Func<bool> itemIsInputting;

        private Func<bool> itemIsLocking;

        #region Constructors

        public ViewItemLogic(IViewItem item, IViewItemFrame itemFrame, IViewItemInteraction itemInteraction, IViewItemDataModel itemModel, IViewItemLayout itemLayout)
            : base(item)
        {
            if (itemFrame == null)
            {
                throw new ArgumentNullException("itemFrame");
            }

            if (itemInteraction == null)
            {
                throw new ArgumentNullException("itemInteraction");
            }

            if (itemModel == null)
            {
                throw new ArgumentNullException("itemModel");
            }

            if (itemLayout == null)
            {
                throw new ArgumentNullException("itemLayout");
            }

            this.ItemFrame       = itemFrame;
            this.ItemInteraction = itemInteraction;
            this.ItemModel       = itemModel;
            this.ItemLayout      = itemLayout;
        }

        #endregion

        #region Destructors

        ~ViewItemLogic()
        {
            this.Dispose();
        }

        #endregion

        #region IDisposable

        public override void Dispose()
        {
            if (this.ItemFrame != null)
            {
                this.ItemFrame.Dispose();
            }

            if (this.ItemInteraction != null)
            {
                this.ItemInteraction.Dispose();
            }

            if (this.ItemModel != null)
            {
                this.ItemModel.Dispose();
            }

            if (this.ItemLayout != null)
            {
               this.ItemLayout.Dispose();
            }

            base.Dispose();
        }

        #endregion

        #region Dependency

        public IViewItemDataModel ItemModel { get; set; }

        public IViewItemFrame ItemFrame { get; set; }

        public IViewItemInteraction ItemInteraction { get; set; }

        public IViewItemLayout ItemLayout { get; set; }

        #endregion

        #region Layer

        public override void SetupLayer()
        {
            base.SetupLayer();

            this.SetupLogic();

            this.ItemFrame      .SetupLayer();
            this.ItemModel      .SetupLayer();
            this.ItemLayout     .SetupLayer();
            this.ItemInteraction.SetupLayer();
        }

        #endregion

        #region State Logic

        private void SetupLogic()
        {
            if (this.ItemIsInputting == null)
            {
                this.ItemIsInputting = 
                    () => this.Item[ItemState.Item_Is_Active]() &&
                          this.Item[ItemState.Item_Is_Selected]() &&
                         !this.Item[ItemState.Item_Is_Editing]();
            }

            if (this.ItemIsLocking == null)
            {
                this.ItemIsLocking = () =>
                    this.Item[ItemState.Item_Is_Editing]() || 
                    this.Item[ItemState.Item_Is_Pending]();
            }
        }

        public Func<bool> ItemIsInputting
        {
            get
            {
                return this.itemIsInputting;
            }
            set
            {
                this.itemIsInputting = value;
                this.Item[ItemState.Item_Is_Inputting] = value;
            }
        }

        public Func<bool> ItemIsLocking
        {
            get { return this.itemIsLocking; }
            set
            {
                this.itemIsLocking = value;
                this.Item[ItemState.Item_Is_Locking] = value;
            }
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            base.Update(time);

            // Reduce lagging graphics 
            if (!this.isFrameUpdated)
            {
                this.isFrameUpdated = true;

                this.ItemFrame.Update(time);
            }
            else
            {
                // For better performance
                if (this.Item[ItemState.Item_Is_Active]())
                {
                    this.ItemFrame.Update(time);
                    this.ItemModel.Update(time);
                }
            }
        }

        public override void UpdateBackwardBuffer()
        {
            base.UpdateBackwardBuffer();

            // This is order insensitive
            this.ItemLayout     .UpdateBackwardBuffer();
            this.ItemFrame      .UpdateBackwardBuffer();
            this.ItemModel      .UpdateBackwardBuffer();
            this.ItemInteraction.UpdateBackwardBuffer();
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (!this.Item[ItemState.Item_Is_Active]())
            {
                return;
            }

            // Mouse
            if (this.View.ViewSettings.MouseEnabled)
            {
                this.ItemFrame.UpdateInput(input, time);
            }

            // Keyboard
            if (this.View.ViewSettings.KeyboardEnabled)
            {
                this.ItemModel.UpdateInput(input, time);
            }

            // Keyboard
            if (this.View.ViewSettings.KeyboardEnabled)
            {
                var keyboard = input.State.Keyboard;

                if (this.Item[ItemState.Item_Is_Inputting]())
                {
                    if (keyboard.IsActionTriggered(KeyboardActions.CommonEditItem))
                    {
                        this.View[ViewState.View_Is_Editing] = () => true;
                        this.Item[ItemState.Item_Is_Pending] = () => true;
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.CommonDeleteItem))
                    {
                        this.DeleteItem();
                    }

                    // Pending status
                    if (this.Item[ItemState.Item_Is_Pending]())
                    {
                        if (keyboard.IsActionTriggered(KeyboardActions.Escape))
                        {
                            this.View[ViewState.View_Is_Editing] = () => false;
                            this.Item[ItemState.Item_Is_Pending] = () => false;
                        }
                    }
                }

                if (!this.Item[ItemState.Item_Is_Locking]())
                {
                    // Extra components
                }
            }
        }

        public void UpdateView(GameTime time)
        {
            this.ItemInteraction.Update(time);
        }

        #endregion Update

        #region Operations

        public void DeleteItem()
        {
            this.View.ItemsWrite.Remove(this.Item);

            this.View.ViewLogic.ViewBinding.RemoveData(this.Item);

            this.Item.Dispose();
        }

        #endregion
    }
}