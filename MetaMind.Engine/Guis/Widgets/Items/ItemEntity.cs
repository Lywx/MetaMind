// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemEntity.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    using Microsoft.Xna.Framework;

    using IDrawable = MetaMind.Engine.IDrawable;

    public interface IItemEntity : IInputable, IDrawable
    {
        bool[] States { get; }

        ICloneable ItemSettings { get; }

        void Disable(ItemState state);

        void Enable(ItemState state);

        bool IsEnabled(ItemState state);

        void UpdateView(GameTime gameTime);
    }

    public abstract class ItemEntity : GameControllableEntity, IItemEntity
    {
        private readonly bool[] states;

        protected ItemEntity(ICloneable itemSettings)
        {
            this.ItemSettings = itemSettings;
            this.states = new bool[(int)ItemState.StateNum];
        }

        public ICloneable ItemSettings { get; protected set; }

        public bool[] States
        {
            get
            {
                return this.states;
            }
        }

        #region State Operation

        public void Disable(ItemState state)
        {
            state.DisableStateIn(this.states);
        }

        public void Enable(ItemState state)
        {
            state.EnableStateIn(this.states);
        }

        public bool IsEnabled(ItemState state)
        {
            return state.IsStateEnabledIn(this.states);
        }

        #endregion State Operation

        #region Update and Draw

        public abstract void UpdateView(GameTime gameTime);

        #endregion Update and Draw
    }
}