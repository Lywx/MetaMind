// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemEntity.cs">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Item
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;

    public abstract class ViewItemStateControl : Control, IViewItemEntity
    {
        protected ViewItemStateControl(ControlManager manager) : base(manager)
        {
            for (var i = 0; i < (int)ViewItemState.StateNum; i++)
            {
                this.itemStates[i] = () => false;
            }
        }

        #region States

        private readonly Func<bool>[] itemStates = new Func<bool>[(int)ViewItemState.StateNum];

        public bool[] ItemStates
        {
            get
            {
                return this.itemStates.Select(state => state()).ToArray();
            }
        }

        public Func<bool> this[ViewItemState state]
        {
            get
            {
                return this.itemStates[(int)state];
            }

            set
            {
                this.itemStates[(int)state] = value;
            }
        }

        #endregion States

        #region Update and Draw

        public abstract void UpdateView(GameTime gameTime);

        #endregion Update and Draw
    }
}