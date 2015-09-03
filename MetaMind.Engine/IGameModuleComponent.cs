namespace MetaMind.Engine
{
    public interface IGameModuleComponent<out TModuleSettings>
    {
        TModuleSettings Settings { get; }

        void Initialize();
    }
}