namespace MetaMind.Engine.Components
{
    using MetaMind.Engine.Guis.Console;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    internal class GameNullInterop : IGameInterop
    {
        public IAudioManager Audio { get; private set; }

        public ContentManager Content { get; private set; }

        public GameConsole Console { get; set; }

        public IFileManager File { get; private set; }

        public GameEngine Engine { get; private set; }

        public IEventManager Event { get; private set; }

        public IGameManager Game { get; private set; }

        public IProcessManager Process { get; private set; }

        public IScreenManager Screen { get; private set; }

        public ISaveManager Save { get; set; }

        public void Initialize()
        {
        }

        public void OnExiting()
        {
        }

        public void UpdateInput(GameTime gameTime)
        {
        }
    }
}