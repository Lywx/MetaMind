// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameEntity.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Elements
{
    using System;
    using System.Linq;

    using MetaMind.Engine.Components.Inputs;

    public abstract class FrameEntity : GameControllableEntity, IFrameEntity
    {
        #region Flyweight Dependency

        protected IInputEvent InputEvent { get; private set; }

        protected IInputState InputState { get; private set; }

        #endregion Service

        #region Constructors and Destructors

        protected FrameEntity()
        {
            this.InputEvent = this.GameInput.Event;
            this.InputState = this.GameInput.State;

            for (var i = 0; i < (int)FrameState.StateNum; i++)
            {
                this.states[i] = () => false;
            }
        }

        #endregion Constructors and Destructors

        #region State Data

        private readonly Func<bool>[] states = new Func<bool>[(int)FrameState.StateNum];

        public bool[] States
        {
            get
            {
                return this.states.Select(state => state()).ToArray();
            }
        }

        public Func<bool> this[FrameState state]
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

        #endregion State Data
    }
}