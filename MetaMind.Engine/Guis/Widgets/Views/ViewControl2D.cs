// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewControl2D.cs" company="UESTC">
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

    public class ViewControl2D : ViewControl1D, IViewControl2D
    {
        public ViewControl2D(IView view, ViewSettings2D viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
            this.Swap      = new ViewSwapControl(this.View, this.ViewSettings, this.ItemSettings);
            this.Scroll    = new ViewScrollControl2D(this.View, this.ViewSettings, this.ItemSettings);
            this.Selection = new ViewSelectionControl2D(this.View, this.ViewSettings, this.ItemSettings);
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

        public override void UpdateInput(GameTime gameTime)
        {
            this.UpdateMouseScroll();
            this.UpdateKeyboardMotion();
            this.UpdateItemInput(gameTime);
        }

        protected override void UpdateKeyboardMotion()
        {
            if (this.AcceptInput)
            {
                // keyboard
                // --------------------------------------------------------------
                if (this.ViewSettings.KeyboardEnabled)
                {
                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Up))
                    {
                        this.MoveUp();
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Down))
                    {
                        this.MoveDown();
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.FastUp))
                    {
                        this.SuperMoveUp();
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.FastDown))
                    {
                        this.SuperMoveDown();
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Left))
                    {
                        this.MoveLeft();
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Right))
                    {
                        this.MoveRight();
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.FastLeft))
                    {
                        this.SuperMoveLeft();
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.FastRight))
                    {
                        this.SuperMoveRight();
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Escape))
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