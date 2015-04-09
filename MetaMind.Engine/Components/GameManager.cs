// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameManager.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;

    using Game = MetaMind.Engine.Game;

    public class GameManager
    {
        #region Singleton

        private static GameManager singleton;

        public static GameManager GetInstance(GameEngine engine)
        {
            return singleton ?? (singleton = new GameManager(engine));
        }

        #endregion Singleton

        private readonly GameComponentCollection components;

        private readonly List<IGame> games = new List<IGame>();

        #region Constructors

        private GameManager(GameEngine engine)
        {
            this.components = engine.Components;
        }

        #endregion

        public void Add(IGame game)
        {
            this.games     .Add(game);
            this.components.Add(game);
        }

        public void OnExiting()
        {
            this.games.ForEach(component => component.OnExiting());
        }
    }
}