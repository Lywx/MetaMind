// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointViewHorizontalLogic.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using System;

    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Items.Factories;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Layouts;
    using MetaMind.Engine.Guis.Widgets.Views.Scrolls;
    using MetaMind.Engine.Guis.Widgets.Views.Selections;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Swaps;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class PointViewHorizontalLogic<TData> : ViewLogic<TData>, IPointViewHorizontalLogic
    {
        private readonly PointViewHorizontalSettings viewSettings;

        protected PointViewHorizontalLogic(
            IView                    view,
            IList<TData>             viewData,
            IViewScrollController    viewScroll,
            IViewSelectionController viewSelection,
            IViewSwapController      viewSwap,
            IViewLayout              viewLayout,
            IViewItemFactory itemFactory)
            : base(view, viewData, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory)
        {
            var viewLayer = this.ViewGetLayer<PointViewHorizontalLayer>();
            this.viewSettings = viewLayer.ViewSettings;

            this.View[ViewState.View_Has_Focus] = this.View[ViewState.View_Has_Selection] = () => viewLayer.ViewSelection.HasSelected;
        }

        #region View Logic Property Injection

        public new IPointViewHorizontalSelectionController ViewSelection
        {
            get
            {
                // Local Default
                if (base.ViewSelection == null)
                {
                    base.ViewSelection = new PointViewHorizontalSelectionController(this.View);
                }

                return (IPointViewHorizontalSelectionController)base.ViewSelection;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                
                if (base.ViewSelection != null)
                {
                    throw new InvalidOperationException();
                }

                this.ViewSelection = value;
            }
        }

        public new IPointViewHorizontalScrollController ViewScroll
        {
            get
            {
                // Local Default
                if (base.ViewScroll == null)
                {
                    base.ViewScroll = new PointViewHorizontalScrollController(this.View);
                }

                return (IPointViewHorizontalScrollController)base.ViewScroll;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                
                if (base.ViewScroll != null)
                {
                    throw new InvalidOperationException();
                }

                this.ViewScroll = value;
            }
        }

        public new IPointViewHorizontalSwapController ViewSwap
        {
            get
            {
                // Local Default
                if (base.ViewSwap == null)
                {
                    base.ViewSwap = new PointViewHorizontalSwapController<TData>(this.View, (IList<TData>) this.ViewData);
                }

                return (IPointViewHorizontalSwapController)base.ViewSwap;
            } 
        }

        #endregion

        #region Operations

        public void AddItem()
        {
            var item = new ViewItem(this.View, this.View.ItemSettings, this.ItemFactory);
            this.View.Items.Add(item);
        }

        public virtual void MoveLeft()
        {
            if (this.viewSettings.Direction == ViewDirection.Inverse)
            {
                // Invert for left scrolling view
                this.ViewSelection.MoveRight();
            }
            else
            {
                this.ViewSelection.MoveLeft();
            }
        }

        public virtual void MoveRight()
        {
            if (this.viewSettings.Direction == ViewDirection.Inverse)
            {
                // Invert for left scrolling view
                this.ViewSelection.MoveLeft();
            }
            else
            {
                this.ViewSelection.MoveRight();
            }
        }

        public void FastMoveLeft()
        {
            for (var i = 0; i < this.viewSettings.ColumnNumDisplay; i++)
            {
                this.MoveLeft();
            }
        }

        public void FastMoveRight()
        {
            for (var i = 0; i < this.viewSettings.ColumnNumDisplay; i++)
            {
                this.MoveRight();
            }
        }

        #endregion Operations

        #region Update Structure

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

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (input.State.Keyboard.IsActionPressed(KeyboardActions.CommonCreateItem))
            {
                this.AddItem();
            }

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
            if (this.View[ViewState.View_Is_Inputting]())
            {
                // Keyboard
                if (this.viewSettings.KeyboardEnabled)
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
                        this.ViewSelection.Cancel();
                    }
                }
            }
        }

        protected virtual void UpdateInputOfMouse(IGameInputService input, GameTime time)
        {
            if (this.View[ViewState.View_Is_Inputting]())
            {
                // Mouse
                if (this.viewSettings.MouseEnabled)
                {
                    if (input.State.Mouse.IsWheelScrolledUp)
                    {
                        this.ViewScroll.MoveLeft();
                    }

                    if (input.State.Mouse.IsWheelScrolledDown)
                    {
                        this.ViewScroll.MoveRight();
                    }
                }
            }
        }

        #endregion Update Input
    }
}