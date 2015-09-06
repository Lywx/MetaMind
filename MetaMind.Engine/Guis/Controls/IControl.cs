namespace MetaMind.Engine.Guis.Controls
{
    public interface IControl
    {
        bool Initialized { get; }

        void Initialize();
    }
}