// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointViewHorizontalLogic.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using System;
    using System.Linq;

    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Items.Factories;
    using MetaMind.Engine.Guis.Widgets.Views.Extensions;
    using MetaMind.Engine.Guis.Widgets.Views.PointView;
    using MetaMind.Engine.Guis.Widgets.Views.Scrolls;
    using MetaMind.Engine.Guis.Widgets.Views.Selections;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Swaps;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewLogic : ViewComponent, IViewLogic
    {
        protected ViewLogic(IView view)
            : base(view)
        {
        }

        public IViewItemFactory ItemFactory { get; private set; }
    }

    public class PointViewHorizontalLogic : ViewComponent, IPointViewHorizontalLogic
    {
        private readonly PointViewHorizontalSettings viewSettings;

        public PointViewHorizontalLogic(IView view, IViewItemFactory itemFactory)
            : base(view)
        {
            this.viewSettings = this.ViewExtension.Get<PointViewHorizontalExtension>().ViewSettings;

            this.ItemFactory = itemFactory;

            this.ViewSwap      = new ViewSwapControl(this.View);
            this.ViewScroll    = new PointViewHorizontalScrollControl(this.View);
            this.ViewSelection = new PointViewHorizontalSelectionControl(this.View);
        }

        // TODO: this is wrong

        protected PointViewHorizontalLogic(
            IView      view,
            IViewItemFactory itemFactory,
            IPointViewHorizontalScrollControl viewScroll,
            IPointViewHorizontalSelectionControl viewSelection,
            viewSwap)
            : base(view)
        {
            if (itemFactory == null)
            {
                throw new ArgumentNullException("itemFactory");
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

            this.ItemFactory = itemFactory;

            this.ViewScroll    = viewScroll;
            this.ViewSelection = viewSelection;
            this.ViewSwap      = viewSwap;

            this.View[ViewState.View_Has_Focus] = this.View[ViewState.View_Has_Selection] = this.ViewSelection.HasSelected();

            this.viewSettings = this.ViewExtension.Get<PointViewHorizontalExtension>().ViewSettings;
        }

        #region Dependency

        public IViewItemFactory ItemFactory { get; protected set; }

        public IPointViewHorizontalScrollControl ViewScroll { get; protected set; }

        public IPointViewHorizontalSelectionControl ViewSelection { get; protected set; }

        public PointViewHorizontalSwap ViewSwap { get; protected set; }

        #endregion

        #region Operations

        public void AddItem()
        {
            var item = new ViewItem(this.View, this.ItemSettings, this.ItemFactory);
            this.View.ViewItems.Add(item);
        }

        public virtual void MoveLeft()
        {
            if (this.viewSettings.Direction == PointViewHorizontalDirection.Inverse)
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
            if (this.viewSettings.Direction == PointViewHorizontalDirection.Inverse)
            {
                // Invert for left scrolling view
                this.ViewSelection.MoveLeft();
            }
            else
            {
                this.ViewSelection.MoveRight();
            }
        }

        // TODO: TO MOVE
        public virtual void SortItems(PointViewSortMode sortMode)
        {
            switch (sortMode)
            {
                case PointViewSortMode.Name:
                    {
                        this.View.ViewItems = this.View.ViewItems.OrderBy(item => item.ItemData.Name).ToList();
                        this.View.ViewItems.ForEach(item => item.ItemLogic.Id = this.View.ViewItems.IndexOf(item));
                    }

                    break;

                case PointViewSortMode.Id:
                    {
                        this.View.ViewItems = this.View.ViewItems.OrderBy(item => item.ItemLogic.Id).ToList();
                        this.View.ViewItems.ForEach(item => item.ItemLogic.Id = this.View.ViewItems.IndexOf(item));
                    }

                    break;
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

        public bool Active
        {
            get { return this.View[ViewState.View_Is_Active](); }
        }

        public override void Update(GameTime time)
        {
            if (this.View[ViewState.View_Is_Active]())
            {
                // TODO: Possible thread safety issue
                foreach (var item in this.View.ViewItems.ToArray())
                {
                    item.Update(time);
                }
            }
            else
            {
                foreach (var item in this.View.ViewItems)
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
                      !this.View[ViewState.View_Is_Editing]() && 
                       this.View[ViewState.View_Has_Focus]();
            }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            // /TODO: REMMOVE or change?
            if (input.State.Keyboard.IsActionPressed(KeyboardActions.TaskCreateItem))
            {
                this.AddItem();
            }

            this.UpdateInputOfMouse(input, time);
            this.UpdateInputOfItems(input, time);
        }

        protected void UpdateInputOfItems(IGameInputService input, GameTime time)
        {
            foreach (var item in this.View.ViewItems.ToArray())
            {
                item.UpdateInput(input, time);
            }
        }

        protected virtual void UpdateInputOfKeyboard(IGameInputService input, GameTime time)
        {
            if (this.AcceptInput)
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
                        this.ViewSelection.Clear();
                    }
                }
            }
        }

        protected virtual void UpdateInputOfMouse(IGameInputService input, GameTime time)
        {
            if (this.AcceptInput)
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