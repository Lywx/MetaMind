namespace MetaMind.Engine
{
    public interface IGameEngineBuilder
    {
        GameEngine Create(string content);
    }
}