namespace MetaMind.Engine.Components
{
    public interface IMMMvcComponentComponent<out TMvcSettings>
    {
        TMvcSettings Settings { get; }

        void Initialize();
    }
}