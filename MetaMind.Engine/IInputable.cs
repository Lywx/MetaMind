// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInputable.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using System;

    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public interface IInputable : IUpdateable
    {
        event EventHandler<EventArgs> ControllableChanged;

        event EventHandler<EventArgs> InputOrderChanged;

        int InputOrder { get; }

        void UpdateInput(IGameInputService input, GameTime gameTime);
    }
}