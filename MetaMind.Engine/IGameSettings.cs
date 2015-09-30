namespace MetaMind.Engine
{
    using System.Collections.Generic;

    public interface IGameSettings : IDictionary<string, object>
    {
        T Get<T>(string id);
    }
}