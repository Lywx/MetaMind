namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;
    using Service;

    public interface IInputableOperations
    {
        void UpdateInput(IGameInputService input, GameTime time);
    }
}