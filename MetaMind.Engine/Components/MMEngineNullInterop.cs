namespace MetaMind.Engine.Components
{
    using Audio;
    using Content.Asset;
    using File;
    using Interop;
    using Interop.Event;
    using Interop.Process;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Services.Console;

    internal class MMEngineNullInterop : MMInputableComponent, IMMEngineInterop
    {
        public MMEngineNullInterop(MMEngine engine) 
            : base(engine)
        {
        }

        public IAssetManager Asset { get; }

        public IAudioManager Audio { get; }

        public ContentManager Content { get; }

        public MMConsole Console { get; set; }

        public IFileManager File { get; }

        public IMMEventManager Event { get; }

        public new IMMGameManager Game { get; }

        public IProcessManager Process { get; }

        public IMMScreenDirector Screen { get; }

        public IMMSaveManager Save { get; set; }

        public override void Initialize()
        {
        }

        public override void UpdateInput(GameTime time)
        {
        }

        public void OnExiting()
        {
        }
    }
}