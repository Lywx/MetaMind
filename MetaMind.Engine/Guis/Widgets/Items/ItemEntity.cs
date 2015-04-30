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

    public abstract class ItemEntity : GameControllableEntity, IItemEntity
    {
        protected ItemEntity(ICloneable itemSettings)
        {
            this.states = new Func<bool>[(int)ItemState.StateNum];

            this.ItemSettings = itemSettings;
        }

        public dynamic ItemSettings { get; protected set; }

        #region States

        private readonly Func<bool>[] states;

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