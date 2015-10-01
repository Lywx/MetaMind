namespace MetaMind.Engine.Node.Actions.Intervals
{
    using System.Diagnostics;
    using System.Linq;

    public class MMSequence : MMFiniteTimeAction
    {
        #region Constructors

        public MMSequence(
            MMFiniteTimeAction action1,
            MMFiniteTimeAction action2)
            : base(action1.Duration + action2.Duration)
        {
            this.Actions = new MMFiniteTimeAction[2];

            this.Initialize(action1, action2);
        }

        public MMSequence(params MMFiniteTimeAction[] actions)
        {
            this.Actions = new MMFiniteTimeAction[2];

            var prev = actions[0];

            // Can't call base(duration) because we need to calculate duration here
            this.Duration = this.CombinedDuration(actions);

            if (actions.Length == 1)
            {
                this.Initialize(prev, new MMExtraAction());
            }
            else
            {
                // Basically what we are doing here is creating a whole bunch of 
                // nested MMSequences from the actions.
                for (var i = 1; i < actions.Length - 1; i++)
                {
                    prev = new MMSequence(prev, actions[i]);
                }

                this.Initialize(prev, actions[actions.Length - 1]);
            }
        }

        #endregion Constructors

        public MMFiniteTimeAction[] Actions { get; }

        #region Initialization

        private void Initialize(
            MMFiniteTimeAction actionOne,
            MMFiniteTimeAction actionTwo)
        {
            Debug.Assert(actionOne != null);
            Debug.Assert(actionTwo != null);

            this.Actions[0] = actionOne;
            this.Actions[1] = actionTwo;
        }

        #endregion

        #region Duration Operations

        private float CombinedDuration(MMFiniteTimeAction[] actions)
        {
            return actions.Sum(action => action.Duration);
        }

        #endregion

        #region Operations

        protected internal override MMActionState StartAction(MMNode target)
        {
            return new MMSequenceState(this, target);
        }

        public override MMFiniteTimeAction Reverse()
        {
            return new MMSequence(
                this.Actions[1].Reverse(),
                this.Actions[0].Reverse());
        }

        #endregion
    }
}
