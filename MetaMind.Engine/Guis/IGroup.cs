namespace MetaMind.Engine.Guis
{
    public interface IGroup<out TGroupSettings> : IGameControllableEntity
    {
        TGroupSettings Settings { get; }

        IGroupControl Control { get; }

        IGroupGraphics Graphics { get; }
    }
}