namespace MetaMind.Engine.Services
{
    using MetaMind.Engine.Components.Inputs;

    using Microsoft.Xna.Framework;

    public interface IGameInputService : IGameComponent
    {
        IInputEvent Event { get; }

        IInputState State { get; }
    }
}