using MetaMind.Engine.Components;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine
{
    public class EngineComponent : DrawableGameComponent
    {
        protected EngineComponent( Game game )
            : base( game )
        {

        }

        protected MessageManager MessageManager
        {
            get { return Engine.MessageManager; }
        }

        protected FolderManager FolderManager
        {
            get { return Engine.FolderManager; }
        }

        protected ScreenManager ScreenManager
        {
            get { return Engine.ScreenManager; }
        }
    }
}