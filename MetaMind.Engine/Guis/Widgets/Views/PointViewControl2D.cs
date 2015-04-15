// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointViewControl2D.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class PointViewControl2D : PointViewControl1D, IPointViewControl2D
    {
        public PointViewControl2D(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
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

        public virtual void SuperMoveDown()
        {
            for (var i = 0; i < this.ViewSettings.RowNumDisplay; i++)
            {
                this.MoveDown();
            }
        }

        public virtual void SuperMoveUp()
        {
            for (var i = 0; i < this.ViewSettings.RowNumDisplay; i++)
            {
                this.MoveUp();
            }
        }

        #endregion Operations

        #region Update Input

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            this.UpdateMouseScroll(gameInput);
            this.UpdateKeyboardMotion(gameInput);
            this.UpdateItemInput(gameInput, gameTime);
        }

        protected override void UpdateKeyboardMotion(IGameInput gameInput)
        {
            if (this.AcceptInput)
            {
                // keyboard
                // --------------------------------------------------------------
                if (this.ViewSettings.KeyboardEnabled)
                {
                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.Up))
                    {
                        this.MoveUp();
                    }

                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.Down))
                    {
                        this.MoveDown();
                    }

                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.FastUp))
                    {
                        this.SuperMoveUp();
                    }

                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.FastDown))
                    {
                        this.SuperMoveDown();
                    }

                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.Left))
                    {
                        this.MoveLeft();
                    }

                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.Right))
                    {
                        this.MoveRight();
                    }

                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.FastLeft))
                    {
                        this.SuperMoveLeft();
                    }

                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.FastRight))
                    {
                        this.SuperMoveRight();
                    }

                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.Escape))
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