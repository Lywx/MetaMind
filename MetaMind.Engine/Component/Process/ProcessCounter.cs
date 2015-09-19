namespace MetaMind.Engine.Component.Process
{
    using Microsoft.Xna.Framework;

    public class ProcessCounter : GameEntity
    {
        public ProcessCounter(int totalFrame)
        {
            this.TotalFrame = totalFrame;
            this.LastFrame = totalFrame - 1;

            this.CurrentFrame = 0;
        }

        public int TotalFrame { get; set; }

        public int LastFrame { get; set; }

        public int CurrentFrame { get; set; }

        public float Progress
        {
            get { return (float)this.CurrentFrame / this.TotalFrame; }
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            if (this.CurrentFrame < this.TotalFrame)
            {
                ++this.CurrentFrame;
            }
        }
    }
}