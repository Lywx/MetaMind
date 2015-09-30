// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameManager.cs">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using System;
    using Microsoft.Xna.Framework;
    using NLog;

    public class GameManager : GameComponent, IGameManager
    {
#if DEBUG
        #region Logger

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion
#endif

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
#if DEBUG
            logger.Info($"Added {this} to GameEngine.Components.");
#endif
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