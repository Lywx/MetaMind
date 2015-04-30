namespace MetaMind.Engine.Guis
{
    public interface IGroup<out TGroupSettings> : IGameControllableEntity
    {
        TGroupSettings Settings { get; }

        IGroupLogic Logic { get; }

        IGroupVisual Visual { get; }
    }
}