namespace MetaMind.Engine.Components.Audio
{
    using System.IO;
    using Microsoft.Xna.Framework.Audio;

    public static class MMAudioManagerFactory
    { 
        public static MMAudioManager Create(MMEngine engine)
        {
            var content = engine.Content.RootDirectory;

            // Current audio requirement is quite easy to use this simple 
            // implementation without relying on asset manager. 
            var audioSettings     = Path.Combine(content, @"Audio\Win\Audio.xgs");
            var waveBankSettings  = Path.Combine(content, @"Audio\Win\Wave Bank.xwb");
            var soundBankSettings = Path.Combine(content, @"Audio\Win\Sound Bank.xsb");

            var audioEngine = new AudioEngine(audioSettings);
            var waveBank    = new WaveBank(audioEngine, waveBankSettings);
            var soundBank   = new SoundBank(audioEngine, soundBankSettings);

            return new MMAudioManager(engine, audioEngine, waveBank, soundBank)
            {
                UpdateOrder = int.MaxValue
            };
        }
    }
}