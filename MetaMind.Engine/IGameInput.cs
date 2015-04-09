namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;

    public interface IGameInput
    {
        InputEventManager Event { get; }

        InputSequenceManager Sequence { get; }
    }
}