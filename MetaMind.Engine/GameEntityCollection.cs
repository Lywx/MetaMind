namespace MetaMind.Engine
{
    using System.Collections.Generic;
    using Services;
    using Microsoft.Xna.Framework;

    public class GameEntityCollection<T> : List<T>
        where T : IGameEntity
    {
        #region Buffer

        public void UpdateBackwardBuffer()
        {
            foreach (var entity in this)
            {
                entity.UpdateBackwardBuffer();
            }
        }

        public void UpdateForwardBuffer()
        {
            foreach (var entity in this)
            {
                entity.UpdateForwardBuffer();
            }
        }

        #endregion

        #region Load and Unload

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

        #endregion

        #region Update

        public void Update(GameTime time)
        {
            foreach (var entity in this)
            {
                entity.Update(time);
            }
        }

        #endregion
    }
}