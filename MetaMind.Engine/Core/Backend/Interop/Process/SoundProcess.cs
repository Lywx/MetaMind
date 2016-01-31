namespace MetaMind.Engine.Core.Backend.Interop.Process
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;

    public class SoundProcess : MMProcess
    {
        private readonly SoundEffectInstance soundInstance;

        public SoundProcess(SoundEffect sound, )
        {
            this.soundInstance = sound.CreateInstance();
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

        public override void OnSucceed()
        {

        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            if (this.soundInstance.IsDisposed
                || this.soundInstance.State == SoundState.Stopped)
            {
                this.Succeed();
            }
        }
    }
}