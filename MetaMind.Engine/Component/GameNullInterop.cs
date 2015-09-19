namespace MetaMind.Engine.Component
{
    using Audio;
    using Console;
    using Event;
    using File;
    using Graphics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Process;

    internal class GameNullInterop : GameControllableComponent, IGameInterop
    {
        public GameNullInterop(GameEngine engine) 
            : base(engine)
        {
        }        
        
        public IAudioManager Audio { get; private set; }

        public ContentManager Content { get; private set; }

        public GameConsole Console { get; set; }

        public IFileManager File { get; private set; }

        public IEventManager Event { get; private set; }

        public new IGameManager Game { get; private set; }

        public IProcessManager Process { get; private set; }

        public IScreenManager Screen { get; private set; }

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