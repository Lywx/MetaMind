using System.Collections.Generic;

namespace MetaMind.Engine.Components
{
    public class RunnerManager
    {
        #region Singleton

        private static RunnerManager singleton;

        public static RunnerManager GetInstance()
        {
            return singleton ?? ( singleton = new RunnerManager() );
        }

        #endregion Singleton

        private readonly List<EngineRunner> runners = new List<EngineRunner>();

        public void Add( EngineRunner runner )
        {
            runners.Add( runner );
        }

        public void Exit()
        {
            runners.ForEach( component => component.Exit() );
        }
    }
}