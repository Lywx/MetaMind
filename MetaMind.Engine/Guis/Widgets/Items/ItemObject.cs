// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemObject.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    using Microsoft.Xna.Framework;

    public interface IItemObject
    {
        bool[] States { get; }

        ICloneable ItemSettings { get; }

        void Disable(ItemState state);

        void Draw(GameTime gameTime, byte alpha);

        void Enable(ItemState state);

        bool IsEnabled(ItemState state);

        void UpdateInput(GameTime gameTime);

        void UpdateStructureForView(GameTime gameTime);

        void UpdateStructure(GameTime gameTime);
    }

    public abstract class ItemObject : GameEngineAccess, IItemObject
    {
        private readonly bool[] states;

        protected ItemObject(ICloneable itemSettings)
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

        public abstract void Draw(GameTime gameTime, byte alpha);

        public abstract void UpdateInput(GameTime gameTime);

        public abstract void UpdateStructureForView(GameTime gameTime);

        public abstract void UpdateStructure(GameTime gameTime);

        #endregion Update and Draw
    }
}