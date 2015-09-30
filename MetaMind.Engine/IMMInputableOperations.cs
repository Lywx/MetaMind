namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;
    using Service;

    public interface IMMInputableOperations
    {
        void UpdateInput(IMMEngineInputService input, GameTime time);
    }
}