namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine.Components.Events;

    public class MultiplexerGroupSessionSavedListener : Listener
    {
        private readonly MultiplexerGroup multiplexerGroup;

        public MultiplexerGroupSessionSavedListener(MultiplexerGroup multiplexerGroup)
        {
            this.multiplexerGroup = multiplexerGroup;

            this.RegisteredEvents.Add((int)SessionEventType.SessionSaved);
        }

        public override bool HandleEvent(IEvent e)
        {
            this.multiplexerGroup.ScheduleDataReload();

            return true;
        }
    }
}