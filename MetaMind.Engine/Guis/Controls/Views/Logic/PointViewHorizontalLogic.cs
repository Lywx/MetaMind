// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointViewHorizontalLogic.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Controls.Views.Logic
{
    using System;
    using Components.Inputs;
    using Extensions;
    using Items.Factories;
    using Layers;
    using Layouts;
    using Microsoft.Xna.Framework;
    using Scrolls;
    using Selections;
    using Services;
    using Settings;
    using Swaps;

    public class PointViewHorizontalLogic : PointViewLogic, IPointViewHorizontalLogic 
    {
        private PointViewHorizontalSettings viewSettings;

        protected PointViewHorizontalLogic(IView view, IViewScrollController viewScroll, IViewSelectionController viewSelection, IViewSwapController viewSwap, IViewLayout viewLayout, IViewItemFactory itemFactory)
            : base(view, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory)
        {
        }

        public override void SetupLayer()
        {
            var viewLayer = this.ViewGetLayer<PointViewHorizontalLayer>();
            this.viewSettings = viewLayer.ViewSettings;

            try
            {
                var expression = this.View[ViewState.View_Has_Selection].ToExpression();

                if (expression.Equals<bool>(new Func<bool>(() => false).ToExpression()))
                {
                    this.View[ViewState.View_Has_Selection] = () => viewLayer.ViewSelection.HasSelected;
                }
            }
            catch (ArgumentException)
            {
                // Must be already assigned to instance function, so it is desirable not to set the delegate
            }

            try
            {
                var expression = this.View[ViewState.View_Has_Focus].ToExpression();

                if (expression.Equals<bool>(new Func<bool>(() => false).ToExpression()))
                {
                    this.View[ViewState.View_Has_Focus] = this.View[ViewState.View_Has_Selection];
                }
            }
            catch (ArgumentException)
            {
                // Must be already assigned to instance function, so it is desirable not to set the delegate
            }
        }

        #region View Logic Property Injection

        public new IPointViewHorizontalSelectionController ViewSelection
        {
            get
            {
                return (IPointViewHorizontalSelectionController)base.ViewSelection;
            }
        }

        public new IPointViewHorizontalScrollController ViewScroll
        {
            get
            {
                return (IPointViewHorizontalScrollController)base.ViewScroll;
            }
        }

        public new IPointViewHorizontalLayout ViewLayout
        {
            get { return (IPointViewHorizontalLayout)base.ViewLayout; }
        }

        #endregion

        #region Events

        public event EventHandler ScrolledLeft;

        public event EventHandler ScrolledRight;

        public event EventHandler MovedLeft;

        public event EventHandler MovedRight;

        private void OnScrolledLeft()
        {
            if (this.ScrolledLeft != null)
            {
                this.ScrolledLeft(this, EventArgs.Empty);
            }
        }

        private void OnScrolledRight()
        {
            if (this.ScrolledRight != null)
            {
                this.ScrolledRight(this, EventArgs.Empty);
            }
        }

        private void OnMovedLeft()
        {
            if (this.MovedLeft != null)
            {
                this.MovedLeft(this, EventArgs.Empty);
            }
        }

        private void OnMovedRight()
        {
            if (this.MovedRight != null)
            {
                this.MovedRight(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Operations

        public virtual void ScrollLeft()
        {
            this.ViewScroll.MoveLeft();

            this.OnScrolledLeft();
        }

        public virtual void ScrollRight()
        {
            this.ViewScroll.MoveRight();

            this.OnScrolledRight();
        }

        public virtual void MoveLeft()
        {
            if (this.viewSettings.ViewDirection == ViewDirection.Inverse)
            {
                // Invert for left scrolling view
                this.ViewSelection.MoveRight();
            }
            else
            {
                this.ViewSelection.MoveLeft();
            }

            this.OnMovedLeft();
        }

        public virtual void MoveRight()
        {
            if (this.viewSettings.ViewDirection == ViewDirection.Inverse)
            {
                // Invert for left scrolling view
                this.ViewSelection.MoveLeft();
            }
            else
            {
                this.ViewSelection.MoveRight();
            }

            this.OnMovedRight();
        }

        public void FastMoveLeft()
        {
            for (var i = 0; i < this.viewSettings.ViewColumnDisplay; i++)
            {
                this.MoveLeft();
            }
        }

        public void FastMoveRight()
        {
            for (var i = 0; i < this.viewSettings.ViewColumnDisplay; i++)
            {
                this.MoveRight();
            }
        }

        #endregion Operations

        #region Update Input

        protected override void UpdateInputOfKeyboard(IGameInputService input, GameTime time)
        {
            base.UpdateInputOfKeyboard(input, time);

            if (this.View[ViewState.View_Is_Inputting]())
            {
                if (this.View.ViewSettings.KeyboardEnabled)
                {
                    var keyboard = input.State.Keyboard;
                    if (keyboard.IsActionTriggered(KeyboardActions.Left))
                    {
                        this.MoveLeft();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.Right))
                    {
                        this.MoveRight();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.FastLeft))
                    {
                        this.FastMoveLeft();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.FastRight))
                    {
                        this.FastMoveRight();
                    }
                }
            }
        }

        protected override void UpdateInputOfMouse(IGameInputService input, GameTime time)
        {
            if (this.View[ViewState.View_Is_Inputting]())
            {
                if (this.View.ViewSettings.MouseEnabled)
                {
                    var mouse = input.State.Mouse;
                    if (mouse.IsWheelScrolledUp)
                    {
                        this.ScrollLeft();
                    }

                    if (mouse.IsWheelScrolledDown)
                    {
                        this.ScrollRight();
                    }
                }
            }
        }

        #endregion
    }
}