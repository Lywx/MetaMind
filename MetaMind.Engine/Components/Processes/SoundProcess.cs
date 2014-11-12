// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SoundProcess.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Processes
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;

    public class SoundProcess : ProcessBase
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
            soundInstance.Play();
        }

        public override void OnSuccess()
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (soundInstance.IsDisposed || soundInstance.State == SoundState.Stopped)
            {
                Succeed();
            }
        }
    }
}