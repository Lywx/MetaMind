namespace MetaMind.Engine.Component.Process
{
    using Microsoft.Xna.Framework;

    public abstract class StepProcess : Process
    {
        private readonly ProcessCounter counter;

        public StepProcess(int totalFrame)
        {
            this.counter = new ProcessCounter(totalFrame);
        }

        protected int TotalFrame
        {
            get { return this.counter.TotalFrame; }
        }

        protected int LastFrame
        {
            get { return this.counter.LastFrame; }
        }

        protected int CurrentFrame
        {
            get { return this.counter.CurrentFrame; }
        }

        protected float Progress
        {
            get { return this.counter.Progress; }
        }

        #region Update

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.counter.Update(time);
        }

        #endregion
    }
}