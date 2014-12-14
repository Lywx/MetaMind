namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;

    public class MultiplexerGroupCallNotifiedListener : ListenerBase
    {
        public MultiplexerGroupCallNotifiedListener()
        {
            this.RegisteredEvents.Add((int)AdventureEventType.CallNotified); 
        }

        public override bool HandleEvent(EventBase @event)
        {
            GameEngine.AudioManager.PlayMusic("Illusion Mirror");
            return true;
        }
    }
}