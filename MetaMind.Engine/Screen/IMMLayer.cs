namespace MetaMind.Engine.Screen
{
    using System;
    using Entities;

    public interface IMMLayer : IMMInputEntity, IMMLayerOperations 
    {
        IMMScreen Screen { get; }

        #region State

        bool Active { get; }

        #endregion

        #region Events

        event EventHandler FadedIn;

        event EventHandler FadedOut;

        #endregion

        #region Graphics

        byte Opacity { get; }

        #endregion
    }
}