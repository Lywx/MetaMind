namespace MetaMind.Engine.Screen
{
    using System;

    public interface IGameLayer : IGameInputableEntity, IGameLayerOperations 
    {
        IGameScreen Screen { get; }

        #region State

        bool Active { get; }

        #endregion

        #region Events

        event EventHandler FadedIn;

        event EventHandler FadedOut;

        #endregion

        #region Graphics

        byte Alpha { get; }

        #endregion
    }
}