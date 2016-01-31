namespace MetaMind.Engine.Core.Settings
{
    using System.Collections.Generic;

    public interface IMMSettings : IDictionary<string, object>
    {
        T Get<T>(string id);
    }
}