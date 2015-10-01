// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Node.Actions
{
    using System;

    public class MMFiniteTimeActionState : MMActionState
    {
        #region Constructors

        public MMFiniteTimeActionState(MMFiniteTimeAction action, MMNode target)
            : base(action, target)
        {
            this.Duration = action.Duration;
        }

        #endregion

        public bool FirstTick { get; private set; } = true;

        #region Duration Data

        public float Duration { get; set; }

        public float Elapsed { get; private set; } = 0.0f;

        #endregion 

        #region State Data

        public override bool IsDone => this.Elapsed >= this.Duration;

        #endregion

        #region Operations

        /// <remarks>
        /// Step calls Update
        /// </remarks>
        /// <param name="dt"></param>
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

        #endregion
    }
}