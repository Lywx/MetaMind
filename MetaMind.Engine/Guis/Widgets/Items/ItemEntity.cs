// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemEntity.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;
    using System.Linq;

    using Microsoft.Xna.Framework;
    using Settings;

    public abstract class ItemEntity : GameControllableEntity, IItemEntity
    {
        protected ItemEntity()
        {
            for (var i = 0; i < (int)ItemState.StateNum; i++)
            {
                this.states[i] = () => false;
            }
        }

        #region States

        private readonly Func<bool>[] states = new Func<bool>[(int)ItemState.StateNum];

        public bool[] States
        {
            get
            {
                return this.states.Select(state => state()).ToArray();
            }
        }

        public Func<bool> this[ItemState state]
        {
            get
            {
                return this.states[(int)state];
            }

            set
            {
                this.states[(int)state] = value;
            }
        }

        #endregion States

        #region Update and Draw

        public abstract void UpdateView(GameTime gameTime);

        #endregion Update and Draw
    }
}