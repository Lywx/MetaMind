namespace MetaMind.Engine
{
    public interface IGameModuleComponent<out TGroupSettings>
    {
        TGroupSettings Settings { get; }

        void Initialize();
    }
}