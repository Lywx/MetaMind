namespace MetaMind.Engine
{
    using System;

    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public interface IGameEntity : IUpdateable, IDisposable
    {
        void LoadContent(IGameInteropService interop);
        
        void UnloadContent(IGameInteropService interop);

        #region Double Buffer

        void UpdateBuffer();

        #endregion
    }
}