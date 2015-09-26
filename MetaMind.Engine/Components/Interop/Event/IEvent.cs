namespace MetaMind.Engine.Components.Interop.Event
{
    public interface IEvent
    {
        int EventType { get; set; }

        object EventData { get; set; }

        long CreationTime { get; set; }

        int EventLife { get; set; }

        bool Handled { get; set; }

        int HandleAttempts { get; set; }
    }
}