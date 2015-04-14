// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameEntity.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Elements
{
    public interface IFrameBase : IUpdateable, IDrawable, IInputable
    {
        bool[] States { get; }

        bool IsEnabled(FrameState state);

        void Disable(FrameState state);

        void Enable(FrameState state);
    }

    public abstract class FrameEntity : GameControllableEntity, IFrameBase
    {
        #region Constructors

        protected FrameEntity()
        {
            this.states = new bool[(int)FrameState.StateNum];
        }


        #endregion

        #region State Data

        private readonly bool[] states;

        public bool[] States
        {
            get
            {
                return this.states;
            }
        }

        #endregion

        #region State Control

        public bool IsEnabled(FrameState state)
        {
            return state.IsStateEnabledIn(this.states);
        }

        public void Disable(FrameState state)
        {
            state.DisableStateIn(this.states);
        }

        public void Enable(FrameState state)
        {
            state.EnableStateIn(this.states);
        }

        #endregion 
    }
}