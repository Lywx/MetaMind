namespace MetaMind.Engine.Guis
{
    public interface IGroup<TGroupSettings>
    {
        TGroupSettings Settings { get; }

        IGroupControl Control { get; }

        IGroupGraphics Graphics { get; }
    }
}