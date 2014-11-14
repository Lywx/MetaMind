// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WaitProcess.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Processes
{
    using System;

    using Microsoft.Xna.Framework;

    public class WaitProcess : ProcessBase
    {
        public readonly TimeSpan TotalDuration;

        public WaitProcess(TimeSpan duration)
        {
            TotalDuration = duration;
        }

        public TimeSpan Duration { get; private set; }

        public override void Update(GameTime gameTime)
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