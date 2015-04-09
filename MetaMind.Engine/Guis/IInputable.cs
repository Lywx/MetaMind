// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInputable.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis
{
    using System;

    using Microsoft.Xna.Framework;

    public interface IInputable : IUpdateable
    {
        /// <summary>
        ///     Handles the input part of updating.
        /// </summary>
        void UpdateInput(IGameInput gameInput, GameTime gameTime);

        /// <summary>
        ///     Handles the structure part of updating.
        /// </summary>
        void UpdateStructure(GameTime gameTime);

        int InputOrder { get; }

        event EventHandler<EventArgs> InputOrderChanged;

        event EventHandler<EventArgs> ControllableChanged;
    }
}