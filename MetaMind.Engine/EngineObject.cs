using System.Runtime.Serialization;

using MetaMind.Engine.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MetaMind.Engine
{
    using System;

    using MetaMind.Engine.Components.Graphics;

    [DataContract]
    public class EngineObject : IDisposable
    {
        protected static FontManager FontManager { get { return GameEngine.FontManager; } }

        protected static AudioManager AudioManager { get { return GameEngine.AudioManager; } }

        protected static ContentManager ContentManager { get { return GameEngine.ContentManager; } }

        protected static EventManager EventManager { get { return GameEngine.EventManager; } }

        protected static GraphicsManager GraphicsManager { get { return GameEngine.GraphicsManager; } }

        protected static InputEventManager InputEventManager { get { return GameEngine.InputEventManager; } }

        protected static InputSequenceManager InputSequenceManager { get { return GameEngine.InputSequenceManager; } }

        protected static MessageManager MessageManager { get { return GameEngine.MessageManager; } }

        protected static ProcessManager ProcessManager { get { return GameEngine.ProcessManager; } }

        protected static ScreenManager ScreenManager { get { return GameEngine.ScreenManager; } }

        public virtual void Dispose()
        {
        }
    }
}