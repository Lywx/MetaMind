using System.Runtime.Serialization;
using MetaMind.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MetaMind.Engine
{
    [DataContract]
    public class EngineObject
    {
        protected AudioManager          AudioManager         { get { return Engine.AudioManager; } }
        protected ContentManager        ContentManager       { get { return Engine.ContentManager; } }
        protected EventManager          EventManager         { get { return Engine.EventManager; } }
        protected FolderManager         FolderManager        { get { return Engine.FolderManager; } }
        protected FontManager           FontManager          { get { return Engine.FontManager; } }
        protected GraphicsDeviceManager GraphicsManager      { get { return Engine.GraphicsManager; } }
        protected InputEventManager     InputEventManager    { get { return Engine.InputEventManager; } }
        protected InputSequenceManager  InputSequenceManager { get { return Engine.InputSequenceManager; } }
        protected MessageManager        MessageManager       { get { return Engine.MessageManager; } }
        protected ProcessManager        ProcessManager       { get { return Engine.ProcessManager; } }
        protected ScreenManager         ScreenManager        { get { return Engine.ScreenManager; } }
    }
}