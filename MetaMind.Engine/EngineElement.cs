using System;
using MetaMind.Engine.Components;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine
{
    public class EngineElement : DrawableGameComponent
    {
        protected EngineElement( Engine engine )
            : base( engine )
        {
            if ( engine == null )
                throw new ArgumentNullException( "engine" );
            Engine = engine;
            Engine.Elements.Add( this );
            Game.Components.Add( this );
        }

        protected Engine Engine { get; set; }
        protected FolderManager FolderManager
        {
            get { return Engine.FolderManager; }
        }

        protected MessageManager MessageManager
        {
            get { return Engine.MessageManager; }
        }

        protected ScreenManager ScreenManager
        {
            get { return Engine.ScreenManager; }
        }

        public virtual void OnExiting( Object sender, EventArgs args )
        {
        }
    }
}