// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SoundProcess.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Processes
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;

    public class SoundProcess : Process
    {
        private readonly SoundEffectInstance soundInstance;

        public SoundProcess(SoundEffect sound)
        {
            soundInstance = sound.CreateInstance();
        }

        public override void OnAbort()
        {
        }

        public override void OnFail()
        {
        }

        public override void OnInit()
        {
            base.OnInit();

            this.soundInstance.Play();
        }

        public override void OnSuccess()
        {
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            if (this.soundInstance.IsDisposed || this.soundInstance.State == SoundState.Stopped)
            {
                this.Succeed();
            }
        }
    }
}