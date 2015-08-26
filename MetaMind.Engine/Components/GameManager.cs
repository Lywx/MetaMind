// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameManager.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using System;

    using Microsoft.Xna.Framework;

    public class GameManager : IGameManager
    {
        private GameComponentCollection Components { get; set; }

        private IGame Game { get; set; }

        #region Constructors

        public GameManager(GameEngine engine)
        {
            this.Components = engine.Components;
        }

        #endregion

        public void Plug(IGame game)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            if (this.Game != null)
            {
                throw new InvalidOperationException("Game exists already.");
            }

            this.Game = game;

            this.Components.Add(game);
        }

        public void OnExiting()
        {
            this.Game?.OnExiting();
        }

        public void Dispose()
        {
            this.Game?.Dispose();
            this.Game = null;
        }
    }
}