using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Components
{
    public class RunnerManager
    {
        #region Singleton

        private static RunnerManager singleton;

        public static RunnerManager GetInstance( GameEngine engine )
        {
            return singleton ?? ( singleton = new RunnerManager( engine ) );
        }

        #endregion Singleton

        private readonly List<EngineRunner> runners = new List<EngineRunner>();
        private readonly GameComponentCollection components;

        private RunnerManager( GameEngine engine )
        {
            components = engine.Components;
        }

        public void Add( EngineRunner runner )
        {
            runners   .Add( runner );
            components.Add( runner );
        }

        public void Exit()
        {
            runners.ForEach( component => component.Exit() );
        }
    }
}