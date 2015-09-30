// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISessionController.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Session
{
    using System;

    /// <summary>
    /// Session controller is the main control for session management. It should 
    /// handle the communication with game engine and more advanced operations.
    /// </summary>
    public interface ISessionController<TData> : IMMFreeUpdatable, IMMInteroperableOperations, IDisposable
    {
        TData Data { get; set; }
    }
}