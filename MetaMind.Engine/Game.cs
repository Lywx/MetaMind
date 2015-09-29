// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Game.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    public class Game : GameInputableComponent, IGame
    {
        #region Constructors

        protected Game(GameEngine engine)
            : base(engine)
        {
            this.Interop.Game.Add(this);
        }

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