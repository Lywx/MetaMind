namespace MetaMind.Engine
{
    using LightInject;

    public class GameEngineCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<GameEngine>();
            serviceRegistry.Register<IGameEngine, GameEngine>();
        }
    }
}