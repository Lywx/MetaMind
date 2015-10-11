namespace MetaMind.Engine.Entities.Nodes.Actions
{
    using System;

    public class MMFiniteTimeActionState : MMActionState
    {
        #region Constructors

        public MMFiniteTimeActionState(MMFiniteTimeAction action, IMMNode target)
            : base(action, target)
        {
            this.Duration = action.Duration;
        }

        #endregion

        #region Duration Data

        public float Duration { get; set; }

        public float Elapsed { get; private set; } = 0.0f;

        #endregion 

        #region State Data

        public override bool IsDone => this.Elapsed >= this.Duration;

        #endregion

        #region Operations

        protected internal override void Step(float dt)
        {
            this.Elapsed += dt;

            // Avoid divided by zero
            var duration = Math.Max(this.Duration, float.Epsilon);
            var progressActual = this.Elapsed / duration;

            // Avoid progress too much
            var progress = Math.Min(1, progressActual);

            // Avoid progress negative value
            this.Update(Math.Max(0f, progress));
        }

        #endregion
    }
}