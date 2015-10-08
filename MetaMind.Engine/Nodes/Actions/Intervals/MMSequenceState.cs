namespace MetaMind.Engine.Nodes.Actions.Intervals
{
    using System;

    public class MMSequenceState : MMFiniteTimeActionState
    {
        private readonly MMFiniteTimeAction[] actions = new MMFiniteTimeAction[2];

        private readonly MMFiniteTimeActionState[] actionStates = new MMFiniteTimeActionState[2];

        protected int actionPreviousIndex;

        private int actionCurrentIndex;

        /// <summary>
        /// Update parameter for action[0]
        /// </summary>
        private float timeAction1;

        /// <summary>
        /// Update parameter split for the first action. Because MMSequence is a
        /// combination of two actions.
        /// </summary>
        protected float timeAction1Completion;

        /// <summary>
        /// Update parameter for action[1]
        /// </summary>
        private float timeAction2;

        #region Constructors

        public MMSequenceState(MMSequence action, IMMNode target)
            : base(action, target)
        {
            this.actions = action.Actions;

            this.timeAction1Completion = this.actions[0].Duration / this.Duration;
            this.actionPreviousIndex = -1;
        }

        #endregion Constructors

        protected bool HasActionRepeatForever
            => (this.actions[0] is MMRepeatForever)
               || (this.actions[1] is MMRepeatForever);

        protected bool PreviousActionRepeatForever
            => this.actions[this.actionPreviousIndex] is MMRepeatForever;

        public override bool IsDone
        {
            get
            {
                if (this.HasActionRepeatForever
                    && this.PreviousActionRepeatForever)
                {
                    return false;
                }

                return base.IsDone;
            }
        }

        protected internal override void Stop()
        {
            // Issue #1305
            if (this.actionPreviousIndex != -1)
            {
                this.actionStates[this.actionPreviousIndex].Stop();
            }

            base.Stop();
        }

        protected internal override void Step(float dt)
        {
            if (this.actionPreviousIndex > -1
                && this.PreviousActionRepeatForever)
            {
                this.actionStates[this.actionPreviousIndex].Step(dt);
            }
            else
            {
                base.Step(dt);
            }
        }

        public override void Update(float time)
        {
            this.UpdateActionTime(time);

            // When according to the time, action[0] is finished
            if (this.actionCurrentIndex == 1)
            {
                // However action[0] was skipped because the Update is not called
                if (this.actionPreviousIndex == -1)
                {
                    // Do extra work to execute skipped action[0]
                    this.actionStates[0] = (MMFiniteTimeActionState)this.actions[0].StartAction(this.Target);
                    this.actionStates[0].Update(1.0f);
                    this.actionStates[0].Stop();
                }

                // When action[0] ran but incomplete
                else if (this.actionPreviousIndex == 0)
                {
                    // Do extra work to finish action[0]
                    this.actionStates[0].Update(1.0f);
                    this.actionStates[0].Stop();
                }
            }

            // When action[0] hasn't finished and
            else if (this.actionCurrentIndex == 0
                     && this.actionPreviousIndex == 1)
            {
                // Reverse mode ?
                // XXX: BUG this case doesn't contemplate when _last==-1, found=0 and in "reverse mode"
                // since it will require a hack to know if an action is on reverse mode or not.
                // "step" should be overriden, and the "reverseMode" value propagated to inner Sequences.
                this.actionStates[1].Update(0);
                this.actionStates[1].Stop();
            }

            // Last action found and it is done.
            if (this.actionCurrentIndex == this.actionPreviousIndex
                && this.actionStates[this.actionCurrentIndex].IsDone)
            {
                return;
            }

            // Last action found and it is done
            if (this.actionCurrentIndex != this.actionPreviousIndex)
            {
                this.actionStates[this.actionCurrentIndex] = (MMFiniteTimeActionState)this.actions[this.actionCurrentIndex].StartAction(this.Target);
            }

            this.UpdateAction();
        }

        private void UpdateAction()
        {
            this.actionStates[this.actionCurrentIndex].Update(time1);

            this.actionPreviousIndex = this.actionCurrentIndex;
        }

        private void UpdateActionTime(float time)
        {
            // Update progress for action[0], time = progress / completion 
            var timeAction1Progress = time;

            // When action[0] hasn't completed
            if (timeAction1Progress < this.timeAction1Completion)
            {
                this.actionCurrentIndex = 0;

                if (Math.Abs(this.timeAction1Completion) > float.Epsilon)
                {
                    this.timeAction1 = time / this.timeAction1Completion;
                }
                else
                {
                    this.timeAction1 = 1;
                }
            }

            // When action[0] is completed
            else
            {
                this.actionCurrentIndex = 1;

                // When action[0]'s duration cover entire sequence
                if (this.timeAction1Completion == 1)
                {
                    // At a result, action[1] should be finished too
                    this.timeAction2 = 1;
                }
                else
                {
                    var timeAction2Progress = time - this.timeAction1Completion;
                    var timeAction2Completion = 1 - this.timeAction1Completion;
                    this.timeAction2 = timeAction2Progress / timeAction2Completion;
                }
            }
        }
    }
}