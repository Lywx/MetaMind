// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Game.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using System;

    using Microsoft.Xna.Framework;

    public class Game : DrawableGameComponent
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

        public void Run()
        {
            GameEngine.Run();
        }

        public virtual void OnExiting()
        {
        }
    }
}