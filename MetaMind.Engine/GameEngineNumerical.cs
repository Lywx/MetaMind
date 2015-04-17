namespace MetaMind.Engine
{
    using System;

    public class GameEngineNumerical : IGameNumerical
    {
        public GameEngineNumerical()
        {
            this.Random = new Random((int)DateTime.Now.Ticks);
        }

        public Random Random { get; private set; }
    }
}