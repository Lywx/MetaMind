namespace MetaMind.Engine.Guis
{
    using Microsoft.Xna.Framework;

    using IDrawable = MetaMind.Engine.IDrawable;

    public interface IModuleVisualControl : IUpdateable, IDrawable, IInputable, IGameEntity
    {
    }
}