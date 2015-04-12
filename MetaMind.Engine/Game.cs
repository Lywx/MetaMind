// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Game.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;

    public class Game : DrawableGameComponent, IGame
    {
        #region Engine Data

        protected IGameInterop GameInterop { get; set; }

        #endregion

        #region Constructors

        protected Game(GameEngine gameEngine)
            : base(gameEngine)
        {
            this.GameInterop = new GameEngineInterop(gameEngine);
            this.GameInterop.Game.Add(this);
        }

        #endregion

        #region IGame

        public virtual void OnExiting()
        {
        }

        public void Run()
        {
            this.GameInterop.GameEngine.Run();
        }

        #endregion
    }
}