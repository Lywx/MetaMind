namespace MetaMind.Engine.Services
{
    using System;

    public class GameEngineNumericalService : IGameNumericalService
    {
        public GameEngineNumericalService()
        {
            this.Random = new Random((int)DateTime.Now.Ticks);
        }

        public Random Random { get; private set; }
    }
}