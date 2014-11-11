using Microsoft.Xna.Framework;
using System;

namespace MetaMind.Engine
{
    public class EngineRunner : DrawableGameComponent
    {
        protected EngineRunner( GameEngine engine )
            : base( engine )
        {
            if ( engine == null )
                throw new ArgumentNullException( "engine" );
            GameEngine = engine;
            GameEngine.RunnerManager.Add( this );
        }

        protected GameEngine GameEngine { get; private set; }
        public void Run()
        {
            GameEngine.Run();
        }

        public virtual void OnExiting()
        {
        }
    }
}