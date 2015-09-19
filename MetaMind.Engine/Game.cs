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
    using Service;

    public class Game : DrawableGameComponent, IGame
    {
        #region Constructors

        protected Game(GameEngine engine)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            this.Engine  = engine;
            this.Interop = engine.Interop;
            this.Interop.Game.Add(this);
        }

        #endregion

        #region Engine Data

        protected GameEngine Engine { get; set; }

        protected IGameInteropService Interop { get; set; }

        #endregion

        #region IGame

        public virtual void OnExiting()
        {
        }

        public void Run()
        {
            this.Engine.Run();
        }

        #endregion
    }
}