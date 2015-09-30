namespace MetaMind.Engine
{
    public interface IMMMvcComponentComponent<out TMvcSettings>
    {
        TMvcSettings Settings { get; }

        void Initialize();
    }
}