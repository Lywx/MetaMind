// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MMGame.cs">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    public class MMGame : MMInputableComponent, IMMGame
    {
        #region Constructors

        protected MMGame(MMEngine engine)
            : base(engine)
        {
            this.Interop.Game.Add(this);
        }

        #endregion

        #region IMMGame

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