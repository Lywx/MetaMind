// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EngineRunner.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using System;

    using Microsoft.Xna.Framework;

    public class EngineRunner : DrawableGameComponent
    {
        protected EngineRunner(GameEngine engine)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException("engine");
            }

            GameEngine = engine;
            GameEngine.RunnerManager.Add(this);
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