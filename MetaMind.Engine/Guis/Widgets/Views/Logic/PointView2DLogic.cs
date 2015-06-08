// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointView2DLogicControl.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;

    using Components.Inputs;
    using Items.Data;
    using Items.Factories;
    using Layers;
    using Layouts;
    using Scrolls;
    using Selections;
    using Services;
    using Settings;
    using Swaps;

    public class PointView2DLogic : PointViewHorizontalLogic, IPointView2DLogic 
    {
        private PointView2DSettings viewSettings;

        public PointView2DLogic(
            IView view,
            IViewScrollController viewScroll,
            IViewSelectionController viewSelection,
            IViewSwapController viewSwap,
            IViewLayout viewLayout,
            IViewItemBinding itemBinding,
            IViewItemFactory itemFactory)
            : base(view, viewScroll, viewSelection, viewSwap, viewLayout, itemBinding, itemFactory)
        {
        }

        #region Indirect Dependency

        protected PointView2DSettings ViewSettings
        {
            get { return this.viewSettings; }
        }

        #endregion

        #region Layer Injecetion 

        public new IPointView2DLayout ViewLayout
        {
            get
            {
                return (IPointView2DLayout)base.ViewLayout;
            }
        }

        public new IPointView2DSelectionController ViewSelection
        {
            get
            {
                return (IPointView2DSelectionController)base.ViewSelection;
            }
        }

        public new IPointView2DScrollController ViewScroll
        {
            get
            {
                return (IPointView2DScrollController)base.ViewScroll;
            }
        }

        IPointViewVerticalSelectionController IPointViewVerticalLogic.ViewSelection
        {
            get
            {
                return this.ViewSelection;
            }
        }

        IPointViewVerticalScrollController IPointViewVerticalLogic.ViewScroll
        {
            get
            {
                return this.ViewScroll;
            }
        }

        IPointViewVerticalLayout IPointViewVerticalLogic.ViewLayout
        {
            get
            {
                return this.ViewLayout;
            }
        }

        #endregion

        #region Events

        public event EventHandler ScrolledUp;

        public event EventHandler ScrolledDown;

        public event EventHandler MovedUp;

        public event EventHandler MovedDown;

        private void OnScrolledUp()
        {
            if (this.ScrolledUp != null)
            {
                this.ScrolledUp(this, EventArgs.Empty);
            }
        }

        private void OnScrolledDown()
        {
            if (this.ScrolledDown != null)
            {
                this.ScrolledDown(this, EventArgs.Empty);
            }
        }

        private void OnMovedUp()
        {
            if (this.MovedUp != null)
            {
                this.MovedUp(this, EventArgs.Empty);
            }
        }

        private void OnMovedDown()
        {
            if (this.MovedDown != null)
            {
                this.MovedDown(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Layer

        public override void SetupLayer()
        {
            base.SetupLayer();

            var viewLayer = this.ViewGetLayer<PointView2DLayer>();
            this.viewSettings = viewLayer.ViewSettings;
        }

        #endregion

        #region Operations

        public virtual void ScrollDown()
        {
            this.ViewScroll.MoveDown();

            this.OnScrolledDown();
        }

        public virtual void ScrollUp()
        {
            this.ViewScroll.MoveUp();

            this.OnScrolledUp();
        }

        public virtual void MoveDown()
        {
            this.ViewSelection.MoveDown();

            this.OnMovedDown();
        }

        public virtual void MoveUp()
        {
            this.ViewSelection.MoveUp();

            this.OnMovedUp();
        }

        public virtual void FastMoveDown()
        {
            for (var i = 0; i < this.ViewSettings.ViewRowDisplay; i++)
            {
                this.MoveDown();
            }
        }

        public virtual void FastMoveUp()
        {
            for (var i = 0; i < this.ViewSettings.ViewRowDisplay; i++)
            {
                this.MoveUp();
            }
        }

        #endregion Operations

        #region Update Input

        protected override void UpdateInputOfMouse(IGameInputService input, GameTime time)
        {
            if (this.View[ViewState.View_Is_Inputting]())
            {
                if (this.ViewSettings.MouseEnabled)
                {
                    var mouse = input.State.Mouse;
                    if (mouse.IsWheelScrolledUp)
                    {
                        this.ScrollUp();
                    }

                    if (mouse.IsWheelScrolledDown)
                    {
                        this.ScrollDown();
                    }
                }
            }
        }

        protected override void UpdateInputOfKeyboard(IGameInputService input, GameTime time)
        {
            if (this.View[ViewState.View_Is_Inputting]())
            {
                if (this.ViewSettings.KeyboardEnabled)
                {
                    var keyboard = input.State.Keyboard;
                    if (keyboard.IsActionTriggered(KeyboardActions.Up))
                    {
                        this.MoveUp();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.Down))
                    {
                        this.MoveDown();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.Left))
                    {
                        this.MoveLeft();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.Right))
                    {
                        this.MoveRight();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.FastUp))
                    {
                        this.FastMoveUp();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.FastDown))
                    {
                        this.FastMoveDown();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.FastLeft))
                    {
                        this.FastMoveLeft();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.FastRight))
                    {
                        this.FastMoveRight();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.Escape))
                    {
                        this.ViewSelection.Cancel();
                    }
                }
            }
        }

        #endregion Update
    }
}