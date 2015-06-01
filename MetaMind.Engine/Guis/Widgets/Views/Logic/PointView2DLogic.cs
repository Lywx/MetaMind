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
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items.Factories;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Layouts;
    using MetaMind.Engine.Guis.Widgets.Views.Scrolls;
    using MetaMind.Engine.Guis.Widgets.Views.Selections;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Swaps;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class PointView2DLogic<TData> : PointViewHorizontalLogic<TData>, IPointView2DLogic
    {
        private readonly PointView2DSettings viewSettings;

        private readonly IPointView2DSelectionController viewSelection;

        public PointView2DLogic(IView view, IList<TData> viewData, IViewScrollController viewScroll, IViewSelectionController viewSelection, IViewSwapController viewSwap, IViewLayout viewLayout, IViewItemFactory itemFactory)
            : base(view, viewData, viewScroll, viewSelection, viewSwap, viewLayout, itemFactory)
        {
            var viewLayer = this.ViewGetLayer<PointView2DLayer>();
            this.viewSettings  = viewLayer.ViewSettings;
            this.viewSelection = viewLayer.ViewSelection;
        }

        #region View Logic Property Injecetion

        public new IPointView2DLayout ViewLayout
        {
            get
            {
                // Local Default
                if (base.ViewLayout == null)
                {
                    base.ViewLayout = new PointView2DLayout(this.View);
                }

                return (IPointView2DLayout)base.ViewLayout;
            }

            protected set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                
                if (base.ViewLayout != null)
                {
                    throw new InvalidOperationException();
                }

                this.ViewLayout = value;
            }
        }

        public new IPointView2DSelectionController ViewSelection
        {
            get
            {
                // Local Default
                if (base.ViewSelection == null)
                {
                    base.ViewSelection = new PointView2DSelectionController(this.View);
                }

                return (IPointView2DSelectionController)base.ViewSelection;
            }

            protected set
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

        public new IPointView2DScrollController ViewScroll
        {
            get
            {
                // Local Default
                if (base.ViewScroll == null)
                {
                    base.ViewScroll = new PointView2DScrollController(this.View);
                }

                return (IPointView2DScrollController)base.ViewScroll;
            }

            protected set
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

        #endregion

        #region Operations

        public virtual void MoveDown()
        {
            this.viewSelection.MoveDown();
        }

        public virtual void MoveUp()
        {
            this.viewSelection.MoveUp();
        }

        public virtual void FastMoveDown()
        {
            for (var i = 0; i < this.viewSettings.RowNumDisplay; i++)
            {
                this.MoveDown();
            }
        }

        public virtual void FastMoveUp()
        {
            for (var i = 0; i < this.viewSettings.RowNumDisplay; i++)
            {
                this.MoveUp();
            }
        }

        #endregion Operations

        #region Update Input

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.UpdateInputOfMouse(input, time);
            this.UpdateInputOfKeyboard(input, time);
            this.UpdateInputOfItems(input, time);
        }

        protected override void UpdateInputOfKeyboard(IGameInputService input, GameTime time)
        {
            if (this.View[ViewState.View_Is_Inputting]())
            {
                // Keyboard
                if (this.viewSettings.KeyboardEnabled)
                {
                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Up))
                    {
                        this.MoveUp();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Down))
                    {
                        this.MoveDown();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Left))
                    {
                        this.MoveLeft();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Right))
                    {
                        this.MoveRight();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.FastUp))
                    {
                        this.FastMoveUp();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.FastDown))
                    {
                        this.FastMoveDown();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.FastLeft))
                    {
                        this.FastMoveLeft();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.FastRight))
                    {
                        this.FastMoveRight();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Escape))
                    {
                        this.ViewSelection.Cancel();
                    }
                }
            }
        }

        #endregion Update
    }
}