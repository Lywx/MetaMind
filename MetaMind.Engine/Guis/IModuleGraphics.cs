namespace MetaMind.Engine.Guis
{
    using Microsoft.Xna.Framework;

    using IDrawable = MetaMind.Engine.IDrawable;

    public interface IModuleGraphics : IUpdateable, IDrawable, IInputable, IGameEntity
    {
    }
}