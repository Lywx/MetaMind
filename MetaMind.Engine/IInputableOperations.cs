namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;
    using Services;

    public interface IInputableOperations
    {
        void UpdateInput(IGameInputService input, GameTime time);
    }
}