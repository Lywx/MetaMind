namespace MetaMind.Next
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal interface IGameRender : IGameComponent
    {
        SpriteBatch SpriteBatch { get; }
    }
}