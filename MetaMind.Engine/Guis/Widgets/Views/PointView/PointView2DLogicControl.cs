// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointView2DLogicControl.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.PointView
{
    using System;

    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class PointView2DLogicControl : PointView1DLogicControl, IPointView2DLogicControl
    {
        public PointView2DLogicControl(IView view, PointView2DSettings viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
            : base(
                view,
                viewSettings,
                itemSettings,
                itemFactory,
                new PointViewSwapControl(view, viewSettings, itemSettings),
                new PointView2DSelectionControl(view, viewSettings, itemSettings),
                new PointView2DScrollControl(view, viewSettings, itemSettings))
        {
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
            this.UpdateInputOfMouse(input, time);
            this.UpdateInputOfKeyboard(input, time);
            this.UpdateInputOfItems(input, time);
        }

        protected override void UpdateInputOfKeyboard(IGameInputService input, GameTime time)
        {
            if (this.AcceptInput)
            {
                // Keyboard
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