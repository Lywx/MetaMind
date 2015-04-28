namespace MetaMind.Engine.Guis
{
    public interface IGroup<out TGroupSettings> : IGameControllableEntity
    {
        TGroupSettings Settings { get; }

        IGroupLogicControl Logic { get; }

        IGroupVisualControl Visual { get; }
    }
}