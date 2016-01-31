namespace MetaMind.Engine.Core.Backend
{
    public interface IMMMvcComponentComponent<out TMVCSettings>
    {
        TMVCSettings Settings { get; }

        void Initialize();
    }
}