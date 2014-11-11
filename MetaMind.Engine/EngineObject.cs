using MetaMind.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Runtime.Serialization;

namespace MetaMind.Engine
{
    [DataContract]
    public class EngineObject
    {
        protected AudioManager          AudioManager         { get { return GameEngine.AudioManager; } }
        protected ContentManager        ContentManager       { get { return GameEngine.ContentManager; } }
        protected EventManager          EventManager         { get { return GameEngine.EventManager; } }
        protected FontManager           FontManager          { get { return GameEngine.FontManager; } }
        protected GraphicsDeviceManager GraphicsManager      { get { return GameEngine.GraphicsManager; } }
        protected InputEventManager     InputEventManager    { get { return GameEngine.InputEventManager; } }
        protected InputSequenceManager  InputSequenceManager { get { return GameEngine.InputSequenceManager; } }
        protected MessageManager        MessageManager       { get { return GameEngine.MessageManager; } }
        protected ProcessManager        ProcessManager       { get { return GameEngine.ProcessManager; } }
        protected ScreenManager         ScreenManager        { get { return GameEngine.ScreenManager; } }
    }
}