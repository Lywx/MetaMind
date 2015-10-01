﻿namespace MetaMind.Engine.Node.Actions.Intervals
{
    /// <summary>
    /// Extra action(empty action) for making a MMSequence or MMSpawn when only adding one action to it.
    /// </summary>
    internal class MMExtraAction : MMFiniteTimeAction
    {
        public override MMFiniteTimeAction Reverse()
        {
            return new MMExtraAction();
        }

        protected internal override MMActionState StartAction(MMNode target)
        {
            return new MMExtraActionState(this, target);
        }

        #region Nested Type: MMExtraActionState

        public class MMExtraActionState : MMFiniteTimeActionState
        {
            public MMExtraActionState(MMExtraAction action, MMNode target)
                : base(action, target) {}

            protected internal override void Step(float dt) {}

            public override void Update(float time) {}
        }

        #endregion Action State
    }
}
