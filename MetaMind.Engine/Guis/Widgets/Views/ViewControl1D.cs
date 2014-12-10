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

    using Microsoft.Xna.Framework;

    public class ViewControl1D : ViewComponent, IViewControl
    {
        public ViewControl1D(IView view, ViewSettings1D viewSettings, ICloneable itemSettings)
            : base(view, viewSettings, itemSettings)
        {
            this.Swap      = new ViewSwapControl(this.View, this.ViewSettings, this.ItemSettings);
            this.Scroll    = new ViewScrollControl1D(this.View, this.ViewSettings, this.ItemSettings);
            this.Selection = new ViewSelectionControl1D(this.View, this.ViewSettings, this.ItemSettings);
        }

        protected ViewControl1D(IView view, ViewSettings2D viewSettings, ICloneable itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        #region Components

        public dynamic Scroll { get; protected set; }

        public dynamic Selection { get; protected set; }

        public IViewSwapControl Swap { get; protected set; }

        #endregion Components

        #region Operations

        public void SortItems(ViewSortMode sortMode)
        {
            switch (sortMode)
            {
                case ViewSortMode.Name:
                    {
                        this.View.Items = this.View.Items.OrderBy(item => item.ItemData.Labels).ToList();
                        this.View.Items.ForEach(item => item.ItemControl.Id = this.View.Items.IndexOf(item));
                    }

                    break;

                case ViewSortMode.Id:
                    {
                        this.View.Items = this.View.Items.OrderBy(item => item.ItemControl.Id).ToList();
                        this.View.Items.ForEach(item => item.ItemControl.Id = this.View.Items.IndexOf(item));
                    }

                    break;
            }
        }

        public virtual void MoveLeft()
        {
            if (this.ViewSettings.Direction == ViewSettings1D.ScrollDirection.Left)
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
            if (this.ViewSettings.Direction == ViewSettings1D.ScrollDirection.Left)
            {
                // invert for left scrolling view
                this.Selection.MoveLeft();
            }
            else
            {
                this.Selection.MoveRight();
            }
        }

        #endregion Operations

        #region Update

        public bool Active
        {
            get { return this.View.IsEnabled(ViewState.View_Active); }
        }

        public virtual bool AcceptInput
        {
            get
            {
                return this.View.IsEnabled(ViewState.View_Active) && 
                       this.View.IsEnabled(ViewState.View_Has_Focus) && 
                      !this.View.IsEnabled(ViewState.Item_Editting);
            }
        }

        public virtual void UpdateInput(GameTime gameTime)
        {
            if (this.AcceptInput)
            {
                // mouse
                // ------------------------------------------------------------------
                if (this.ViewSettings.MouseEnabled)
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

                // keyboard
                // ------------------------------------------------------------------
                if (this.ViewSettings.KeyboardEnabled)
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

                    // escape
                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Escape))
                    {
                        this.Selection.Clear();
                    }
                }
            }

            // item input
            // -----------------------------------------------------------------
            foreach (var item in this.View.Items.ToArray())
            {
                item.UpdateInput(gameTime);
            }
        }

        public virtual void UpdateStructure(GameTime gameTime)
        {
            if (this.View.IsEnabled(ViewState.View_Active))
            {
                this.UpdateViewLogics();
                this.UpdateItemStructure(gameTime);
            }
            else
            {
                foreach (var item in this.View.Items)
                {
                    item.UpdateStructureForView(gameTime);
                }
            }
        }

        protected void UpdateItemStructure(GameTime gameTime)
        {
            foreach (var item in this.View.Items.ToArray())
            {
                item.UpdateStructure(gameTime);
            }
        }

        protected virtual void UpdateViewFocus()
        {
            if (this.View.IsEnabled(ViewState.View_Has_Selection))
            {
                this.View.Enable(ViewState.View_Has_Focus);
            }
            else
            {
                this.View.Disable(ViewState.View_Has_Focus);
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
                this.View.Enable(ViewState.View_Has_Selection);
            }
            else
            {
                this.View.Disable(ViewState.View_Has_Selection);
            }
        }

        #endregion Update
    }
}