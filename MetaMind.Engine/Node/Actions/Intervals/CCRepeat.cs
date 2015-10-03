namespace CocosSharp
{
    using MetaMind.Engine.Node;
    using MetaMind.Engine.Node.Actions;

    public class MMRepeat : MMFiniteTimeAction
    {
        #region Properties

        public bool ActionInstant { get; private set; }

        public uint Times { get; private set; }

        public uint Total { get; private set; }

        public MMFiniteTimeAction InnerAction { get; private set; }

        #endregion Properties


        #region Constructors

        public MMRepeat (MMFiniteTimeAction action, uint times) : base (action.Duration * times)
        {

            Times = times;
            InnerAction = action;

            ActionInstant = action is MMActionInstant;
            //an instant action needs to be executed one time less in the update method since it uses startWithTarget to execute the action
            if (ActionInstant)
            {
                Times -= 1;
            }
            Total = 0;
        }

        #endregion Constructors

        protected internal override MMActionState StartAction(MMNode target)
        {
            return new MMRepeatState (this, target);

        }

        public override MMFiniteTimeAction Reverse ()
        {
            return new MMRepeat (InnerAction.Reverse(), Times);
        }
    }

    public class MMRepeatState : MMFiniteTimeActionState
    {

        protected bool ActionInstant { get; set; }

        protected float NextDt { get; set; }

        protected MMFiniteTimeAction InnerAction { get; set; }

        protected MMFiniteTimeActionState InnerActionState { get; set; }

        protected uint Times { get; set; }

        protected uint Total { get; set; }

        public MMRepeatState (MMRepeat action, MMNode target)
            : base (action, target)
        { 

            InnerAction = action.InnerAction;
            Times = action.Times;
            Total = action.Total;
            ActionInstant = action.ActionInstant;

            NextDt = InnerAction.Duration / Duration;

            InnerActionState = (MMFiniteTimeActionState)InnerAction.StartAction (target);
        }

        protected internal override void Stop ()
        {
            InnerActionState.Stop ();
            base.Stop ();
        }

        // issue #80. Instead of hooking step:, hook update: since it can be called by any
        // container action like Repeat, Sequence, AMMelDeMMel, etc..
        public override void Update (float time)
        {
            if (time >= NextDt)
            {
                while (time > NextDt && Total < Times)
                {
                    InnerActionState.Update (1.0f);
                    Total++;

                    InnerActionState.Stop ();
                    InnerActionState = (MMFiniteTimeActionState)InnerAction.StartAction (Target);
                    NextDt = InnerAction.Duration / Duration * (Total+1f);
                }

                // fix for issue #1288, incorrect end value of repeat
                if (time >= 1.0f && Total < Times)
                {
                    Total++;
                }

                // don't set an instant action back or update it, it has no use because it has no duration
                if (!ActionInstant)
                {
                    if (Total == Times)
                    {
                        InnerActionState.Update (1f);
                        InnerActionState.Stop ();
                    }
                    else
                    {
                        // issue #390 prevent jerk, use right update
                        InnerActionState.Update (time - (NextDt - InnerAction.Duration / Duration));
                    }

                }
            }
            else
            {
                InnerActionState.Update ((time * Times) % 1.0f);
            }
        }

        public override bool IsDone {
            get { return Total == Times; }
        }

    }

}