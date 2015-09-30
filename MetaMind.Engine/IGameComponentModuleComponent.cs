namespace MetaMind.Engine
{
    public interface IGameComponentModuleComponent<out TMvcSettings>
    {
        TMvcSettings Settings { get; }

        void Initialize();
    }
}