﻿// --------------------------------------------------------------------------------------------------------------------
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

    using Microsoft.Xna.Framework;

    public class PointViewControl1D : ViewComponent, IPointViewControl
    {
        public PointViewControl1D(IView view, PointViewSettings1D viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
            : base(view, viewSettings, itemSettings)
        {
            this.ItemFactory = itemFactory;

            this.Swap      = new PointViewSwapControl(View, ViewSettings, this.ItemSettings);
            this.Scroll    = new PointViewScrollControl1D(View, ViewSettings, this.ItemSettings);
            this.Selection = new PointViewSelectionControl1D(View, ViewSettings, this.ItemSettings);
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
            if (ViewSettings.Direction == PointViewSettings1D.ScrollDirection.Left)
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
            if (ViewSettings.Direction == PointViewSettings1D.ScrollDirection.Left)
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

        public void SuperMoveLeft()
        {
            for (var i = 0; i < ViewSettings.ColumnNumDisplay; i++)
            {
                this.MoveLeft();
            }
        }

        public void SuperMoveRight()
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
            get { return View.IsEnabled(ViewState.View_Active); }
        }

        public override void Update(GameTime gameTime)
        {
            if (View.IsEnabled(ViewState.View_Active))
            {
                this.UpdateViewLogics();
                this.UpdateItem(gameTime);
            }
            else
            {
                foreach (var item in View.Items)
                {
                    item.UpdateView(gameTime);
                }
            }
        }

        protected virtual void UpdateViewFocus()
        {
            if (View.IsEnabled(ViewState.View_Has_Selection))
            {
                View.Enable(ViewState.View_Has_Focus);
            }
            else
            {
                View.Disable(ViewState.View_Has_Focus);
            }
        }

        protected virtual void UpdateViewLogics()
        {
            this.UpdateViewSelection();
            this.UpdateViewFocus();
        }

        protected virtual void UpdateViewSelection()
        {
            if (this.Selection.HasSelected)
            {
                View.Enable(ViewState.View_Has_Selection);
            }
            else
            {
                View.Disable(ViewState.View_Has_Selection);
            }
        }

        #endregion Update Structure

        #region Update Input

        public virtual bool AcceptInput
        {
            get
            {
                return View.IsEnabled(ViewState.View_Active) &&
                       View.IsEnabled(ViewState.View_Has_Focus) &&
                       !View.IsEnabled(ViewState.Item_Editting);
            }
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            this.UpdateMouseScroll(gameInput);
            this.UpdateItemInput(gameInput, gameTime);
        }

        protected void UpdateItemInput(IGameInput gameInput, GameTime gameTime)
        {
            // item input
            // -----------------------------------------------------------------
            foreach (var item in View.Items.ToArray())
            {
                item.UpdateInput(gameInput, gameTime);
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

        protected virtual void UpdateKeyboardMotion(IGameInput gameInput)
        {
            if (this.AcceptInput)
            {
                // keyboard
                // ---------------------------------------------------------------------
                if (ViewSettings.KeyboardEnabled)
                {
                    // movement
                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.Left))
                    {
                        this.MoveLeft();
                    }

                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.Right))
                    {
                        this.MoveRight();
                    }

                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.FastLeft))
                    {
                        this.SuperMoveLeft();
                    }

                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.FastRight))
                    {
                        this.SuperMoveRight();
                    }

                    // escape
                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.Escape))
                    {
                        this.Selection.Clear();
                    }
                }
            }
        }

        protected virtual void UpdateMouseScroll(IGameInput gameInput)
        {
            if (this.AcceptInput)
            {
                // mouse
                // ------------------------------------------------------------------
                if (ViewSettings.MouseEnabled)
                {
                    if (gameInput.State.Mouse.IsWheelScrolledUp)
                    {
                        this.Scroll.MoveLeft();
                    }

                    if (gameInput.State.Mouse.IsWheelScrolledDown)
                    {
                        this.Scroll.MoveRight();
                    }
                }
            }
        }

        #endregion Update Input
    }
}