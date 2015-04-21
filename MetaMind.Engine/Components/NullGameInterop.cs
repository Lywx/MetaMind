namespace MetaMind.Engine.Components
{
    using Microsoft.Xna.Framework;

    internal class NullGameInterop : IGameInterop
    {
        public IAudioManager Audio { get; private set; }

        public IGameEngine Engine { get; private set; }

        public IEventManager Event { get; private set; }

        public IGameManager Game { get; private set; }

        public IProcessManager Process { get; private set; }

        public IScreenManager Screen { get; private set; }

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