using System;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Components.Processes
{
    public class WaitProcess : ProcessBase
    {

        public readonly TimeSpan TotalDuration;
        public TimeSpan Duration { get; private set; }

        public WaitProcess(TimeSpan duration)
        {
            TotalDuration = duration;
        }

        public override void OnUpdate(GameTime gameTime)
        {
            Duration -= gameTime.ElapsedGameTime;
            if (Duration <= TimeSpan.Zero)
            {
                Succeed();
            }
        }

        public override void OnInit()
        {
            base.OnInit();
            Duration = TotalDuration;
        }

        public override void OnSuccess()
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
