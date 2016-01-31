namespace MetaMind.Engine.Core.Entity.Node.Actions.Instants
{
    using Entity.Node.Model;

    public class MMActionInstant : MMFiniteTimeAction
    {
        #region Constructors

        protected MMActionInstant() {}

        #endregion Constructors

        protected internal override MMActionState StartAction(IMMNode target)
        {
            return new MMActionInstantState(this, target);
        }

        public override MMFiniteTimeAction Reverse()
        {
            return new MMActionInstant();
        }
    }

    public class MMActionInstantState : MMFiniteTimeActionState
    {
        public MMActionInstantState(MMActionInstant action, IMMNode target)
            : base(action, target) {}

        public override bool IsDone => true;

        protected internal override void Step(float dt)
        {
            this.Update(1);
        }

        public override void Update(float time)
        {
            // Ignored
        }
    }
}
