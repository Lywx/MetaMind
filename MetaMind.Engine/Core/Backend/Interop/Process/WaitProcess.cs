namespace MetaMind.Engine.Core.Backend.Interop.Process
{
    using System;
    using Microsoft.Xna.Framework;

    public class WaitProcess : MMProcess
    {
        public readonly TimeSpan TotalDuration;

        public WaitProcess(TimeSpan duration)
        {
            this.TotalDuration = duration;
        }

        public TimeSpan Duration { get; private set; }

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.Duration -= time.ElapsedGameTime;
            if (this.Duration <= TimeSpan.Zero)
            {
                this.Succeed();
            }
        }

        public override void OnInit()
        {
            base.OnInit();

            this.Duration = this.TotalDuration;
        }

        public override void OnSucceed()
        {
        }

        public override void OnFail()
        {
        }

        public override void OnAbort()
        {
        }
    }
}