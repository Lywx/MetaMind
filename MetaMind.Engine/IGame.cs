namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;

    public interface IGame : IGameComponent, Microsoft.Xna.Framework.IUpdateable, Microsoft.Xna.Framework.IDrawable
    {
        void Run();

        void OnExiting();
    }
}