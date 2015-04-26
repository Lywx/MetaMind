// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointViewControl1D.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;
    using System.Linq;

    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class PointViewControl1D : ViewComponent, IPointViewControl
    {
        public PointViewControl1D(IView view, PointViewSettings1D viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
            : base(view, viewSettings, itemSettings)
        {
            this.ItemFactory = itemFactory;

            this.Swap      = new PointViewSwapControl(this.View, this.ViewSettings, this.ItemSettings);
            this.Scroll    = new PointViewScrollControl1D(this.View, this.ViewSettings, this.ItemSettings);
            this.Selection = new PointViewSelectionControl1D(this.View, this.ViewSettings, this.ItemSettings);
        }

        protected PointViewControl1D(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
            : base(view, viewSettings, itemSettings)
        {
            this.ItemFactory = itemFactory;
        }

        #region Components

        public IViewItemFactory ItemFactory { get; protected set; }

        public dynamic Scroll { get; protected set; }

        public dynamic Selection { get; protected set; }

        public IPointViewSwapControl Swap { get; protected set; }

        #endregion Components

        #region Operations

        public void AddItem()
        {
            var item = new ViewItemExchangable(View, ViewSettings, ItemSettings, ItemFactory);
            View.Items.Add(item);
        }

        public virtual void MoveLeft()
        {
            if (ViewSettings.Direction == PointViewDirection.Inverse)
            {
                // invert for left scrolling view
                this.Selection.MoveRight();
            }
            else
            {
                this.Selection.MoveLeft();
            }
        }

        public virtual void MoveRight()
        {
            if (ViewSettings.Direction == PointViewDirection.Inverse)
            {
                // invert for left scrolling view
                this.Selection.MoveLeft();
            }
            else
            {
                this.Selection.MoveRight();
            }
        }

        public virtual void SortItems(PointViewSortMode sortMode)
        {
            switch (sortMode)
            {
                case PointViewSortMode.Name:
                    {
                        View.Items = View.Items.OrderBy(item => item.ItemData.Name).ToList();
                        View.Items.ForEach(item => item.ItemControl.Id = View.Items.IndexOf(item));
                    }

                    break;

                case PointViewSortMode.Id:
                    {
                        View.Items = View.Items.OrderBy(item => item.ItemControl.Id).ToList();
                        View.Items.ForEach(item => item.ItemControl.Id = View.Items.IndexOf(item));
                    }

                    break;
            }
        }

        public void FastMoveLeft()
        {
            for (var i = 0; i < ViewSettings.ColumnNumDisplay; i++)
            {
                this.MoveLeft();
            }
        }

        public void FastMoveRight()
        {
            for (var i = 0; i < ViewSettings.ColumnNumDisplay; i++)
            {
                this.MoveRight();
            }
        }
        #endregion Operations

        #region Update Structure

        public bool Active
        {
            get { return View[ViewState.View_Is_Active](); }
        }

        public override void Update(GameTime time)
        {
            if (View[ViewState.View_Is_Active]())
            {
                this.UpdateViewLogics();
                this.UpdateItem(time);
            }
            else
            {
                foreach (var item in View.Items)
                {
                    item.UpdateView(time);
                }
            }
        }

        protected virtual void UpdateViewFocus()
        {
            // TODO: MOVED?
            this.View[ViewState.View_Has_Focus] = () => this.View[ViewState.View_Has_Selection]();
        }

        protected virtual void UpdateViewLogics()
        {
            this.UpdateViewSelection();
            this.UpdateViewFocus();
        }

        protected virtual void UpdateViewSelection()
        {
            // TODO: Move?
            this.View[ViewState.View_Has_Selection] = () => this.Selection.HasSelected();
        }

        #endregion Update Structure

        #region Update Input

        public virtual bool AcceptInput
        {
            get
            {
                return this.View[ViewState.View_Is_Active]() && 
                       this.View[ViewState.View_Has_Focus]() && 
                      !this.View[ViewState.View_Is_Editing]();
            }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.UpdateMouseScroll(input);
            this.UpdateItemInput(input, time);
        }

        protected void UpdateItemInput(IGameInputService input, GameTime gameTime)
        {
            // item input
            // -----------------------------------------------------------------
            foreach (var item in View.Items.ToArray())
            {
                item.UpdateInput(input, gameTime);
            }
        }

        protected void UpdateItem(GameTime gameTime)
        {
            // TODO: Possible thread safety issue
            foreach (var item in View.Items.ToArray())
            {
                item.Update(gameTime);
            }
        }

        protected virtual void UpdateKeyboardMotion(IGameInputService input)
        {
            if (this.AcceptInput)
            {
                // keyboard
                // ---------------------------------------------------------------------
                if (ViewSettings.KeyboardEnabled)
                {
                    // movement
                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Left))
                    {
                        this.MoveLeft();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Right))
                    {
                        this.MoveRight();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.FastLeft))
                    {
                        this.FastMoveLeft();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.FastRight))
                    {
                        this.FastMoveRight();
                    }

                    // escape
                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Escape))
                    {
                        this.Selection.Clear();
                    }
                }
            }
        }

        protected virtual void UpdateMouseScroll(IGameInputService input)
        {
            if (this.AcceptInput)
            {
                // Mouse
                if (ViewSettings.MouseEnabled)
                {
                    if (input.State.Mouse.IsWheelScrolledUp)
                    {
                        this.Scroll.MoveLeft();
                    }

                    if (input.State.Mouse.IsWheelScrolledDown)
                    {
                        this.Scroll.MoveRight();
                    }
                }
            }
        }

        #endregion Update Input
    }
}