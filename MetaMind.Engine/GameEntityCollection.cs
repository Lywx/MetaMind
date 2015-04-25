namespace MetaMind.Engine
{
    using System.Collections.Generic;

    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class GameEntityCollection<T> : List<T>
        where T : IGameEntity
    {
        public void Update(GameTime time)
        {
            foreach (var entity in this)
            {
                entity.Update(time);
            }
        }

        public void LoadContent(IGameInteropService interop)
        {
            foreach (var entity in this)
            {
                entity.LoadContent(interop);
            }
        }

        public void UnloadContent(IGameInteropService interop)
        {
            foreach (var entity in this)
            {
                entity.UnloadContent(interop);
            }
        }
    }
}