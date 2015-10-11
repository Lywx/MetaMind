namespace MetaMind.Engine.Components
{
    public interface IMMMvcComponentComponent<out TMVCSettings>
    {
        TMVCSettings Settings { get; }

        void Initialize();
    }
}