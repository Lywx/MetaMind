namespace MetaMind.Engine.Entities
{
    using Microsoft.Xna.Framework;
    using Service;

    public interface IMMInputOperations
    {
        void UpdateInput(IMMEngineInputService input, GameTime time);
    }
}