// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewControl1D.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
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

    public class ViewControl1D : ViewComponent, IViewControl
    {
        public ViewControl1D(IView view, ViewSettings1D viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
            : base(view, viewSettings, itemSettings)
        {
            this.ItemFactory = itemFactory;

            this.Swap      = new ViewSwapControl(View, ViewSettings, this.ItemSettings);
            this.Scroll    = new ViewScrollControl1D(View, ViewSettings, this.ItemSettings);
            this.Selection = new ViewSelectionControl1D(View, ViewSettings, this.ItemSettings);
        }

        protected ViewControl1D(IView view, ViewSettings2D viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
            : base(view, viewSettings, itemSettings)
        {
            this.ItemFactory = itemFactory;
        }

        #region Components

        public IViewItemFactory ItemFactory { get; protected set; }

        public dynamic Scroll { get; protected set; }

        public dynamic Selection { get; protected set; }

        public IViewSwapControl Swap { get; protected set; }

        #endregion Components

        #region Operations

        public void AddItem()
        {
            var item = new ViewItemExchangable(View, ViewSettings, this.ItemSettings, this.ItemFactory);
            View.Items.Add(item);
        }

        public virtual void MoveLeft()
        {
            if (ViewSettings.Direction == ViewSettings1D.ScrollDirection.Left)
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
            if (ViewSettings.Direction == ViewSettings1D.ScrollDirection.Left)
            {
                // invert for left scrolling view
                this.Selection.MoveLeft();
            }
            else
            {
                this.Selection.MoveRight();
            }
        }

        public virtual void SortItems(ViewSortMode sortMode)
        {
            switch (sortMode)
            {
                case ViewSortMode.Name:
                    {
                        View.Items = View.Items.OrderBy(item => item.ItemData.Name).ToList();
                        View.Items.ForEach(item => item.ItemControl.Id = View.Items.IndexOf(item));
                    }

                    break;

                case ViewSortMode.Id:
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

        public override void UpdateStructure(GameTime gameTime)
        {
            if (View.IsEnabled(ViewState.View_Active))
            {
                this.UpdateViewLogics();
                this.UpdateItemStructure(gameTime);
            }
            else
            {
                foreach (var item in View.Items)
                {
                    item.UpdateStructureForView(gameTime);
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

        public override void UpdateInput(GameTime gameTime)
        {
            this.UpdateMouseScroll();
            this.UpdateItemInput(gameTime);
        }

        protected void UpdateItemInput(GameTime gameTime)
        {
            // item input
            // -----------------------------------------------------------------
            foreach (var item in View.Items.ToArray())
            {
                item.UpdateInput(gameTime);
            }
        }

        protected void UpdateItemStructure(GameTime gameTime)
        {
            // TODO: Possible thread safety issue
            foreach (var item in View.Items.ToArray())
            {
                item.UpdateStructure(gameTime);
            }
        }

        protected virtual void UpdateKeyboardMotion()
        {
            if (this.AcceptInput)
            {
                // keyboard
                // ---------------------------------------------------------------------
                if (ViewSettings.KeyboardEnabled)
                {
                    // movement
                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Left))
                    {
                        this.MoveLeft();
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Right))
                    {
                        this.MoveRight();
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.FastLeft))
                    {
                        this.SuperMoveLeft();
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.FastRight))
                    {
                        this.SuperMoveRight();
                    }

                    // escape
                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Escape))
                    {
                        this.Selection.Clear();
                    }
                }
            }
        }

        protected virtual void UpdateMouseScroll()
        {
            if (this.AcceptInput)
            {
                // mouse
                // ------------------------------------------------------------------
                if (ViewSettings.MouseEnabled)
                {
                    if (InputSequenceManager.Mouse.IsWheelScrolledUp)
                    {
                        this.Scroll.MoveLeft();
                    }

                    if (InputSequenceManager.Mouse.IsWheelScrolledDown)
                    {
                        this.Scroll.MoveRight();
                    }
                }
            }
        }

        #endregion Update Input
    }
}