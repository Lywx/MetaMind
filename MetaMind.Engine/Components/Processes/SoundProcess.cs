using Microsoft.Xna.Framework.Audio;

namespace MetaMind.Engine.Components.Processes
{
    public class SoundProcess : ProcessBase
    {
        private readonly SoundEffectInstance soundInstance;

        public SoundProcess( SoundEffect sound )
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

        public override void OnUpdate( Microsoft.Xna.Framework.GameTime gameTime )
        {
            if ( soundInstance.IsDisposed || soundInstance.State == SoundState.Stopped )
                Succeed();
        }
    }
}