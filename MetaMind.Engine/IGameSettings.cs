namespace MetaMind.Engine
{
    public interface IGameSettings
    {
        T Get<T>(string id);
    }
}