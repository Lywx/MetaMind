namespace MetaMind.Engine.Components
{
    using Microsoft.Xna.Framework;

    public interface ISaveManager : IGameComponent
    {
        void Save();

        void Load();
    }
}