// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointView1DLogicControl.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.PointView
{
    using System;
    using System.Linq;

    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class PointView1DLogicControl : ViewComponent, IPointView1DLogicControl
    {
        public PointView1DLogicControl(IView view, ICloneable viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
            : base(view, viewSettings, itemSettings)
        {
            this.ItemFactory = itemFactory;

            this.Swap      = new PointViewSwapControl(this.View, this.ViewSettings, this.ItemSettings);
            this.Scroll    = new PointViewHorizontalScrollControl(this.View, this.ViewSettings, this.ItemSettings);
            this.Selection = new PointView1DSelectionControl(this.View, this.ViewSettings, this.ItemSettings);
        }

        protected PointView1DLogicControl(
            IView      view,
            ICloneable viewSettings,
            ICloneable       itemSettings,
            IViewItemFactory itemFactory,
            dynamic scroll,
            dynamic selection,
            dynamic swap)
            : base(view, viewSettings, itemSettings)
        {
            if (itemFactory == null)
            {
                throw new ArgumentNullException("itemFactory");
            }

            if (scroll == null)
            {
                throw new ArgumentNullException("scroll");
            }

            if (selection == null)
            {
                throw new ArgumentNullException("selection");
            }

            if (swap == null)
            {
                throw new ArgumentNullException("swap");
            }

            this.ItemFactory = itemFactory;

            this.Scroll      = scroll;
            this.Selection   = selection;
            this.Swap        = swap;

            this.View[ViewState.View_Has_Focus] = this.View[ViewState.View_Has_Selection] = this.Selection.HasSelected();
        }

        #region Components

        public IViewItemFactory ItemFactory { get; protected set; }

        public dynamic Scroll { get; protected set; }

        public dynamic Selection { get; protected set; }

        public dynamic Swap { get; protected set; }

        #endregion Components

        #region Operations

        public void AddItem()
        {
            var item = new ViewItemExchangable(this.View, this.ViewSettings, this.ItemSettings, this.ItemFactory);
            this.View.Items.Add(item);
        }

        public virtual void MoveLeft()
        {
            if (this.ViewSettings.Direction == PointView1DDirection.Inverse)
            {
                // Invert for left scrolling view
                this.Selection.MoveRight();
            }
            else
            {
                this.Selection.MoveLeft();
            }
        }

        public virtual void MoveRight()
        {
            if (this.ViewSettings.Direction == PointView1DDirection.Inverse)
            {
                // Invert for left scrolling view
                this.Selection.MoveLeft();
            }
            else
            {
                this.Selection.MoveRight();
            }
        }

        // TODO: TO MOVE
        public virtual void SortItems(PointViewSortMode sortMode)
        {
            switch (sortMode)
            {
                case PointViewSortMode.Name:
                    {
                        this.View.Items = this.View.Items.OrderBy(item => item.ItemData.Name).ToList();
                        this.View.Items.ForEach(item => item.ItemLogic.Id = this.View.Items.IndexOf(item));
                    }

                    break;

                case PointViewSortMode.Id:
                    {
                        this.View.Items = this.View.Items.OrderBy(item => item.ItemLogic.Id).ToList();
                        this.View.Items.ForEach(item => item.ItemLogic.Id = this.View.Items.IndexOf(item));
                    }

                    break;
            }
        }

        public void FastMoveLeft()
        {
            for (var i = 0; i < this.ViewSettings.ColumnNumDisplay; i++)
            {
                this.MoveLeft();
            }
        }

        public void FastMoveRight()
        {
            for (var i = 0; i < this.ViewSettings.ColumnNumDisplay; i++)
            {
                this.MoveRight();
            }
        }

        #endregion Operations

        #region Update Structure

        public bool Active
        {
            get { return this.View[ViewState.View_Is_Active](); }
        }

        public override void Update(GameTime time)
        {
            if (this.View[ViewState.View_Is_Active]())
            {
                // TODO: Possible thread safety issue
                foreach (var item in this.View.Items.ToArray())
                {
                    item.Update(time);
                }
            }
            else
            {
                foreach (var item in this.View.Items)
                {
                    item.UpdateView(time);
                }
            }
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
            this.UpdateInputOfMouse(input, time);
            this.UpdateInputOfItems(input, time);
        }

        protected void UpdateInputOfItems(IGameInputService input, GameTime time)
        {
            foreach (var item in this.View.Items.ToArray())
            {
                item.UpdateInput(input, time);
            }
        }

        protected virtual void UpdateInputOfKeyboard(IGameInputService input, GameTime time)
        {
            if (this.AcceptInput)
            {
                // Keyboard
                if (this.ViewSettings.KeyboardEnabled)
                {
                    // Movement
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

                    // Escape
                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Escape))
                    {
                        this.Selection.Clear();
                    }
                }
            }
        }

        protected virtual void UpdateInputOfMouse(IGameInputService input, GameTime time)
        {
            if (this.AcceptInput)
            {
                // Mouse
                if (this.ViewSettings.MouseEnabled)
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