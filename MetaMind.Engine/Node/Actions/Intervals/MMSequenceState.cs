// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Node.Actions.Intervals
{
    public class MMSequenceState : MMFiniteTimeActionState
    {
        protected int last;
        protected MMFiniteTimeAction[] actionSequences = new MMFiniteTimeAction[2];
        protected MMFiniteTimeActionState[] actionStates = new MMFiniteTimeActionState[2];
        protected float split;
        private bool hasInfiniteAction = false;

        public MMSequenceState (MMSequence action, MMNode target)
            : base (action, target)
        { 
            this.actionSequences = action.Actions;
            this.hasInfiniteAction = (this.actionSequences [0] is MMRepeatForever) || (this.actionSequences [1] is MMRepeatForever);
            this.split = this.actionSequences [0].Duration / this.Duration;
            this.last = -1;

        }

        public override bool IsDone {
            get {
                if (this.hasInfiniteAction && this.actionSequences [this.last] is MMRepeatForever)
                {
                    return false;
                }

                return base.IsDone;
            }
        }


        protected internal override void Stop ()
        {
            // Issue #1305
            if (this.last != -1)
            {
                this.actionStates [this.last].Stop ();
            }
            base.Stop();
        }

        protected internal override void Step (float dt)
        {
            if (this.last > -1 && (this.actionSequences [this.last] is MMRepeatForever))
            {
                this.actionStates [this.last].Step (dt);
            }
            else
            {
                base.Step (dt);
            }
        }

        public override void Update (float time)
        {
            int found;
            float new_t;

            if (time < this.split)
            {
                // action[0]
                found = 0;
                if (this.split != 0)
                    new_t = time / this.split;
                else
                    new_t = 1;
            }
            else
            {
                // action[1]
                found = 1;
                if (this.split == 1)
                    new_t = 1;
                else
                    new_t = (time - this.split) / (1 - this.split);
            }

            if (found == 1)
            {
                if (this.last == -1)
                {
                    // action[0] was skipped, execute it.
                    this.actionStates [0] = (MMFiniteTimeActionState)this.actionSequences [0].StartAction (this.Target);
                    this.actionStates [0].Update (1.0f);
                    this.actionStates [0].Stop ();
                }
                else if (this.last == 0)
                {
                    this.actionStates [0].Update (1.0f);
                    this.actionStates [0].Stop ();
                }
            }
            else if (found == 0 && this.last == 1)
            {
                // Reverse mode ?
                // XXX: Bug. this case doesn't contemplate when _last==-1, found=0 and in "reverse mode"
                // since it will require a hack to know if an action is on reverse mode or not.
                // "step" should be overriden, and the "reverseMode" value propagated to inner Sequences.
                this.actionStates [1].Update (0);
                this.actionStates [1].Stop ();

            }

            // Last action found and it is done.
            if (found == this.last && this.actionStates [found].IsDone)
            {
                return;
            }


            // Last action found and it is done
            if (found != this.last)
            {
                this.actionStates [found] = (MMFiniteTimeActionState)this.actionSequences [found].StartAction (this.Target);
            }

            this.actionStates [found].Update (new_t);
            this.last = found;

        }


    }
}