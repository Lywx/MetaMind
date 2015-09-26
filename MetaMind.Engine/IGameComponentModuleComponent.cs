namespace MetaMind.Engine
{
    public interface IGameComponentModuleComponent<out TModuleSettings>
    {
        TModuleSettings Settings { get; }

        void Initialize();
    }
}