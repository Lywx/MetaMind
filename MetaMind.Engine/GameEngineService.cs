namespace MetaMind.Engine
{
    using System;

    public class GameEngineService
    {
        private static Random random;

        public static Random Random
        {
            get
            {
                return random;
            }
        }

        public static void Provide(Random service)
        {
            random = service;
        }
    }
}