using Microsoft.Xna.Framework;
using System.Collections.Generic;

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

        private readonly GameComponentCollection components;
        private readonly List<EngineRunner>      runners = new List<EngineRunner>();

        private RunnerManager( GameEngine engine )
        {
            components = engine.Components;
        }

        public void Add( EngineRunner runner )
        {
            runners   .Add( runner );
            components.Add( runner );
        }

        public void OnExiting()
        {
            runners.ForEach( component => component.OnExiting() );
        }
    }
}