namespace MetaMind.Engine.Component
{
    using Audio;
    using Console;
    using Content.Asset;
    using File;
    using Interop;
    using Interop.Event;
    using Interop.Process;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    internal class GameNullInterop : GameControllableComponent, IGameInterop
    {
        public GameNullInterop(GameEngine engine) 
            : base(engine)
        {
        }

        public IAssetManager Asset { get; }

        public IAudioManager Audio { get; }

        public ContentManager Content { get; }

        public GameConsole Console { get; set; }

        public IFileManager File { get; }

        public IEventManager Event { get; }

        public new IGameManager Game { get; }

        public IProcessManager Process { get; }

        public IScreenManager Screen { get; }

        public ISaveManager Save { get; set; }

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