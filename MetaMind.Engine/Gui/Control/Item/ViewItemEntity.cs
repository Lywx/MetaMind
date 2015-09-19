// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemEntity.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Control.Item
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;

    public abstract class ViewItemEntity : Control, IViewItemEntity
    {
        protected ViewItemEntity()
        {
            for (var i = 0; i < (int)ItemState.StateNum; i++)
            {
                this.itemStates[i] = () => false;
            }
        }

        #region States

        private readonly Func<bool>[] itemStates = new Func<bool>[(int)ItemState.StateNum];

        public bool[] ItemStates
        {
            get
            {
                return this.itemStates.Select(state => state()).ToArray();
            }
        }

        public Func<bool> this[ItemState state]
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