namespace MetaMind.Engine.Guis
{
    using IDrawable = MetaMind.Engine.IDrawable;
    using IUpdateable = MetaMind.Engine.IUpdateable;

    public interface IModuleGraphics : IUpdateable, IDrawable, IInputable
    {
    }
}