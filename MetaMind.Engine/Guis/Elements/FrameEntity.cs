// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameEntity.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Elements
{
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

            this.states = new bool[(int)FrameState.StateNum];
        }

        #endregion Constructors and Destructors

        #region State Data

        private readonly bool[] states;

        public bool[] States
        {
            get
            {
                return this.states;
            }
        }

        #endregion State Data

        #region State Control

        public void Disable(FrameState state)
        {
            state.DisableStateIn(this.states);
        }

        public void Enable(FrameState state)
        {
            state.EnableStateIn(this.states);
        }

        public bool IsEnabled(FrameState state)
        {
            return state.IsStateEnabledIn(this.states);
        }

        #endregion State Control
    }
}