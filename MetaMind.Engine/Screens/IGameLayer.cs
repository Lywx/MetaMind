namespace MetaMind.Engine.Screens
{
    using System;

    public interface IGameLayer : IGameControllableEntity, IGameLayerOperations 
    {
        IGameScreen Screen { get; }

        #region State

        bool IsActive { get; }

        #endregion

        #region Events

        event EventHandler FadedIn;

        event EventHandler FadedOut;

        #endregion

        #region Graphics

        byte TransitionAlpha { get; }

        #endregion
    }
}