namespace MetaMind.Engine.Components
{
    public interface IGameManager
    {
        void Plug(IGame game);

        void OnExiting();
    }
}