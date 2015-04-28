// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointView2DLogicControl.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class PointView2DLogicControl : PointView1DLogicControl, IPointView2DLogicControl
    {
        public PointView2DLogicControl(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
            this.Swap      = new PointViewSwapControl(this.View, this.ViewSettings, this.ItemSettings);
            this.Scroll    = new PointViewScrollControl2D(this.View, this.ViewSettings, this.ItemSettings);
            this.Selection = new PointViewSelectionControl2D(this.View, this.ViewSettings, this.ItemSettings);
        }

        #region Operations

        public virtual void MoveDown()
        {
            this.Selection.MoveDown();
        }

        public virtual void MoveUp()
        {
            this.Selection.MoveUp();
        }

        public virtual void FastMoveDown()
        {
            for (var i = 0; i < this.ViewSettings.RowNumDisplay; i++)
            {
                this.MoveDown();
            }
        }

        public virtual void FastMoveUp()
        {
            for (var i = 0; i < this.ViewSettings.RowNumDisplay; i++)
            {
                this.MoveUp();
            }
        }

        #endregion Operations

        #region Update Input

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.UpdateMouseScroll(input);
            this.UpdateKeyboardMotion(input);
            this.UpdateItemInput(input, time);
        }

        protected override void UpdateKeyboardMotion(IGameInputService input)
        {
            if (this.AcceptInput)
            {
                // keyboard
                // --------------------------------------------------------------
                if (this.ViewSettings.KeyboardEnabled)
                {
                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Up))
                    {
                        this.MoveUp();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Down))
                    {
                        this.MoveDown();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.FastUp))
                    {
                        this.FastMoveUp();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.FastDown))
                    {
                        this.FastMoveDown();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Left))
                    {
                        this.MovePrevious();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Right))
                    {
                        this.MoveNext();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.FastLeft))
                    {
                        this.FastMovePrevious();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.FastRight))
                    {
                        this.FastMoveNext();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Escape))
                    {
                        this.Selection.Clear();
                    }
                }
            }
        }

        #endregion Update

        #region Grid Statistics

        public int ColumnNum
        {
            get
            {
                return this.RowNum > 1 ? this.ViewSettings.ColumnNumMax : this.View.Items.Count;
            }
        }

        public int RowNum
        {
            get
            {
                var lastId = this.View.Items.Count - 1;
                return this.RowFrom(lastId) + 1;
            }
        }

        public int ColumnFrom(int id)
        {
            return id % this.ViewSettings.ColumnNumMax;
        }

        public int IdFrom(int i, int j)
        {
            return i * this.ViewSettings.ColumnNumMax + j;
        }

        public int RowFrom(int id)
        {
            for (var row = 0; row < this.ViewSettings.RowNumMax; row++)
            {
                if (id - row * this.ViewSettings.ColumnNumMax >= 0)
                {
                    continue;
                }

                return row - 1;
            }

            return this.ViewSettings.RowNumMax - 1;
        }

        #endregion Grid Statistics
    }
}