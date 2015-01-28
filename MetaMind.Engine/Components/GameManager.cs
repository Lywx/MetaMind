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

        private readonly List<Game> runners = new List<Game>();

        private GameManager(GameEngine engine)
        {
            components = engine.Components;
        }

        public void Add(Game runner)
        {
            runners.Add(runner);
            components.Add(runner);
        }

        public void OnExiting()
        {
            runners.ForEach(component => component.OnExiting());
        }
    }
}