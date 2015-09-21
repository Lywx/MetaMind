namespace MetaMind.Engine
{
    using Collections;
    using Microsoft.Xna.Framework;
    using Service;

    public class GameEntityCollection<T> : ObservableCollection<T>
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

        #region Draw

        public void BeginDraw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            foreach (var entity in this)
            {
                var drawable = entity as IDrawableOperations;
                drawable?.BeginDraw(graphics, time, alpha);
            }
        }

        public void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            foreach (var entity in this)
            {
                var drawable = entity as IDrawableOperations;
                drawable?.Draw(graphics, time, alpha);
            }
        }

        public void EndDraw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            foreach (var entity in this)
            {
                var drawable = entity as IDrawableOperations;
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

        public void UpdateInput(IGameInputService input, GameTime time)
        {
            foreach (var entity in this)
            {
                var inputable = entity as IInputableOperations;
                inputable?.UpdateInput(input, time);
            }
        }

        #endregion
    }
}