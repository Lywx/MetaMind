// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameEntity.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Elements
{
    using MetaMind.Engine.Components.Inputs;

    public interface IFrameBase : IUpdateable, IDrawable, IInputable
    {
        bool[] States { get; }

        void Disable(FrameState state);

        void Enable(FrameState state);

        bool IsEnabled(FrameState state);
    }

    public abstract class FrameEntity : GameControllableEntity, IFrameBase
    {
        #region Service

        private static bool isFlyweightSeviceLoaded;

        private static readonly IInputEvent inputEvent;

        protected static IInputEvent InputEvent { get; private set; }

        protected static IInputState InputState { get; private set; }

        #endregion Service

        #region Constructors and Destructors

        protected FrameEntity()
            : this(GameEngine.Service.GameInput.Event, GameEngine.Service.GameInput.State)
        {
        }

        protected FrameEntity(IInputEvent inputEvent, IInputState inputState)
        {
            if (!isFlyweightSeviceLoaded)
            {
                InputEvent = inputEvent;
                InputState = inputState;

                isFlyweightSeviceLoaded = true;
            }

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