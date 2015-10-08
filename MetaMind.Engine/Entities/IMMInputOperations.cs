namespace MetaMind.Engine.Entities
{
    using Microsoft.Xna.Framework;
    using Services;

    public interface IMMInputOperations
    {
        void UpdateInput(IMMEngineInputService input, GameTime time);
    }
}