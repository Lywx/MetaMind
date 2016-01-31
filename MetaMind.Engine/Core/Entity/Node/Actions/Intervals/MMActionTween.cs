namespace MetaMind.Engine.Core.Entity.Node.Actions.Intervals
{
    using System;
    using Entity.Node.Model;

    public interface IMMActionTweenDelegate
    {
        void UpdateTweenAction(float value, string key);
    }

    public class MMActionTween : MMFiniteTimeAction
    {
        #region Constructors

        public MMActionTween(float duration, string key, float from, float to)
            : base(duration)
        {
            this.Key = key;

            this.From = from;
            this.To   = to;
        }

        public MMActionTween(
            float duration,
            string key,
            float from,
            float to,
            Action<float, string> tweenAction) : this(duration, key, from, to)
        {
            this.TweenAction = tweenAction;
        }

        #endregion Constructors

        #region Properties

        public string Key { get; private set; }

        public float From { get; private set; }

        public float To { get; private set; }

        public Action<float, string> TweenAction { get; private set; }

        #endregion Properties

        protected internal override MMActionState StartAction(IMMNode target)
        {
            return new MMActionTweenState(this, target);
        }

        public override MMFiniteTimeAction Reverse()
        {
            return new MMActionTween(
                this.Duration,
                this.Key,
                this.To,
                this.From,
                this.TweenAction);
        }
    }

    public class MMActionTweenState : MMFiniteTimeActionState
    {
        protected float Delta;

        protected float From { get; private set; }

        protected float To { get; private set; }

        protected string Key { get; private set; }

        protected Action<float, string> TweenAction { get; private set; }

        public MMActionTweenState(MMActionTween action, IMMNode target)
            : base(action, target)
        {
            // TODO(Critical): Maybe I should use dynamic binding here
            if (!(target is IMMActionTweenDelegate))
            {
                throw new ArgumentException("target must implement MMActionTweenDelegate");
            }

            this.TweenAction = action.TweenAction;
            this.From        = action.From;
            this.To          = action.To;
            this.Key         = action.Key;
            this.Delta       = this.To - this.From;
        }

        public override void Update(float time)
        {
            var value = this.To - this.Delta * (1 - time);

            if (this.TweenAction != null)
            {
                this.TweenAction(value, this.Key);
            }
            else if (this.Target is IMMActionTweenDelegate)
            {
                ((IMMActionTweenDelegate)this.Target).UpdateTweenAction(value, this.Key);
            }
        }
    }
}