// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Nodes.Actions
{
    public abstract class MMFiniteTimeAction : MMAction
    {
        #region Constructors

        protected MMFiniteTimeAction()
            : this(0)
        {
        }

        protected MMFiniteTimeAction(float duration)
        {
            this.Duration = duration;
        }

        #endregion Constructors

        #region Duration Data

        float duration;

        public float Duration
        {
            get
            {
                return this.duration;
            }
            set
            {
                // Prevent division by 0
                if (value == 0f)
                {
                    this.duration = float.Epsilon;
                }

                this.duration = value;
            }
        }

        #endregion Properties

        #region Operations

        public abstract MMFiniteTimeAction Reverse();

        protected internal override MMActionState StartAction(IMMNode target)
        {
            return new MMFiniteTimeActionState(this, target);
        }

        #endregion
    }
}