// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Game.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class Game : DrawableGameComponent, IGame
    {
        #region Engine Data

        protected IGameInteropService Interop { get; set; }

        #endregion

        #region Constructors

        protected Game(GameEngine engine)
            : base(engine)
        {
            // Callled after construction of GameEngine
            this.Interop = GameEngine.Service.Interop;
            this.Interop.Game.Plug(this);
        }

        #endregion

        #region IGame

        public virtual void OnExiting()
        {
        }

        public void Run()
        {
            this.Interop.Engine.Run();
        }

        #endregion
    }
}