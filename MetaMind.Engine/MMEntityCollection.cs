namespace MetaMind.Engine
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Service;

    public class MMEntityCollection<T> : List<T>
        where T : IMMEntity
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

        public void LoadContent(IMMEngineInteropService interop)
        {
            foreach (var entity in this)
            {
                entity.LoadContent(interop);
            }
        }

        public void UnloadContent(IMMEngineInteropService interop)
        {
            foreach (var entity in this)
            {
                entity.UnloadContent(interop);
            }
        }

        #endregion

        #region Draw

        public void BeginDraw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            foreach (var entity in this)
            {
                var drawable = entity as IMMDrawableOperations;
                drawable?.BeginDraw(graphics, time, alpha);
            }
        }

        public void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            foreach (var entity in this)
            {
                var drawable = entity as IMMDrawableOperations;
                drawable?.Draw(graphics, time, alpha);
            }
        }

        public void EndDraw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            foreach (var entity in this)
            {
                var drawable = entity as IMMDrawableOperations;
                drawable?.EndDraw(graphics, time, alpha);
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

        public void UpdateInput(IMMEngineInputService input, GameTime time)
        {
            foreach (var entity in this)
            {
                var inputable = entity as IMMInputableOperations;
                inputable?.UpdateInput(input, time);
            }
        }

        #endregion
    }
}