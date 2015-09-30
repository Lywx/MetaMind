// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Actions
{
    using System;

    public class GameFiniteTimeActionState : GameActionState
    {
        public GameFiniteTimeActionState(GameFiniteTimeAction action, MMActor target)
            : base(action, target)
        {
            this.Duration  = action.Duration;
            this.Elapsed   = 0.0f;
            this.FirstTick = true;
        }

        #region Properties

        public bool FirstTick { get; private set; }

        public float Duration { get; set; }

        public float Elapsed { get; private set; }

        #endregion Properties

        #region States

        public override bool IsDone => this.Elapsed >= this.Duration;

        #endregion

        protected internal override void Step(float dt)
        {
            if (this.FirstTick)
            {
                this.FirstTick = false;
                this.Elapsed = 0f;
            }
            else
            {
                this.Elapsed += dt;
            }

            this.Update(Math.Max(0f, Math.Min(1, this.Elapsed / Math.Max(this.Duration, float.Epsilon))));
        }

    }

    public abstract class GameFiniteTimeAction : GameAction
    {
        float duration;

        #region Properties

        public virtual float Duration
        {
            get
            {
                return this.duration;
            }
            set
            {
                float newDuration = value;

                // Prevent division by 0
                if (newDuration == 0)
                {
                    newDuration = float.Epsilon;
                }

                this.duration = newDuration;
            }
        }

        #endregion Properties

        #region Constructors

        protected GameFiniteTimeAction()
            : this(0)
        {
        }

        protected GameFiniteTimeAction(float duration)
        {
            this.Duration = duration;
        }

        #endregion Constructors

        public abstract GameFiniteTimeAction Reverse();

        protected internal override GameActionState StartAction(MMActor target)
        {
            return new GameFiniteTimeActionState(this, target);
        }
    }
}