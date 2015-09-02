// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameManager.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using System;

    using Microsoft.Xna.Framework;

    public class GameManager : GameComponent, IGameManager
    {
        #region Constructors and Finalizer

        public GameManager(GameEngine engine) 
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            this.Engine = engine;
        }

        #endregion

        public new IGame Game { get; private set; }

        protected GameEngine Engine { get; set; }

        public void Add(IGame game)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            if (this.Game != null)
            {
                throw new InvalidOperationException("Game exists already.");
            }

            this.Game = game;

            this.Engine.Components.Add(game);
        }

        public void OnExiting()
        {
            this.Game?.OnExiting();
        }

        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.IsDisposed)
                    {
                        this.Game?.Dispose();
                        this.Game = null;
                    }

                    this.IsDisposed = true;
                }
            }
            catch
            {
                // Ignored
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}