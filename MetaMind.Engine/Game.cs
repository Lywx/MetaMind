// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Game.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using System;

    using Microsoft.Xna.Framework;

    public interface IGame : IGameComponent
    {
        void Run();

        void OnExiting();
    }

    public class Game : DrawableGameComponent, IGame
    {
        protected Game(GameEngine engine)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException("engine");
            }

            GameEngine = engine;
            GameEngine.GameManager.Add(this);
        }

        protected GameEngine GameEngine { get; private set; }

        public virtual void OnExiting()
        {
        }

        public void Run()
        {
            GameEngine.Run();
        }
    }
}