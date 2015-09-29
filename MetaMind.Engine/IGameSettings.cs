namespace MetaMind.Engine
{
    using System.Collections.Generic;
    using Gui.Components;

    public interface IGameSettings : IComponent, IDictionary<string, object>
    {
        T Get<T>(string id);
    }
}