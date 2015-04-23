namespace MetaMind.Engine.Guis
{
    using Microsoft.Xna.Framework;

    using IDrawable = MetaMind.Engine.IDrawable;

    public interface IGroupGraphics : IInputable, IDrawable, IUpdateable
    {
    }
}