namespace MetaMind.Engine.Sessions
{
    using Microsoft.Xna.Framework;

    public interface ISession<TData>
        where TData : ISessionData, new()
    {
        void Save();

        void Update(GameTime gameTime);
    }
}