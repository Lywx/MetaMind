// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEntity.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using System;
    using System.Collections.Generic;

    using MetaMind.Engine.Components.Events;

    using Microsoft.Xna.Framework;

    public class GameEntity : IGameEntity
    {
        #region Event Data

        protected List<IListener> Listeners { get; set; }

        #endregion Event Data

        #region Engine Service

        protected IGameFile GameFile { get; set; }

        protected IGameAudio GameAudio { get; private set; }

        protected IGameInterop GameInterop { get; private set; }

        protected IGameNumerical GameNumerical { get; private set; }

        protected IGameService GameService { get; private set; }

        #endregion 

        #region Constructors

        protected GameEntity()
            : this(GameEngine.Service.GameAudio, null, GameEngine.Service.GameInterop, GameEngine.Service.GameNumerical)
        {
            this.Listeners = new List<IListener>();
        }

        private GameEntity(IGameAudio gameAudio, IGameFile gameFile, IGameInterop gameInterop, IGameNumerical gameNumerical)
        {
            if (gameAudio == null)
            {
                throw new ArgumentNullException("gameAudio");
            }

            if (gameFile == null)
            {
                throw new ArgumentNullException("gameFile");
            }

            if (gameInterop == null)
            {
                throw new ArgumentNullException("gameInterop");
            }

            if (gameNumerical == null)
            {
                throw new ArgumentNullException("gameNumerical");
            }

            this.GameAudio     = gameAudio;
            this.GameFile      = gameFile;
            this.GameInterop   = gameInterop;
            this.GameNumerical = gameNumerical;
        }

        #endregion Constructors

        #region Destructors

        ~GameEntity()
        {
            this.Dispose();
        }

        #endregion Destructors

        #region Events

        public event EventHandler<EventArgs> EnabledChanged;

        public event EventHandler<EventArgs> UpdateOrderChanged;

        protected virtual void OnEnabledChanged(object sender, EventArgs args)
        {
        }

        protected virtual void OnUpdateOrderChanged(object sender, EventArgs args)
        {
        }

        #endregion Events

        #region States

        private bool enabled = true;

        public bool Enabled
        {
            get
            {
                return this.enabled;
            }

            set
            {
                if (this.enabled != value)
                {
                    this.enabled = value;
                    if (this.EnabledChanged != null)
                    {
                        this.EnabledChanged(this, EventArgs.Empty);
                    }

                    this.OnEnabledChanged(this, null);
                }
            }
        }

        #endregion States

        #region Load and Unload

        public virtual void LoadContent(IGameFile gameFile)
        {
        }

        public virtual void LoadGraphics(IGameGraphics gameGraphics)
        {
        }

        public virtual void LoadInterop(IGameInterop gameInterop)
        {
            this.Listeners.ForEach(l => gameInterop.Event.AddListener(l));
        }

        public virtual void UnloadContent(IGameFile gameFile)
        {
        }

        public virtual void UnloadGraphics(IGameGraphics gameGraphics)
        {
        }

        public virtual void UnloadInterop(IGameInterop gameInterop)
        {
            // TODO: UnloadInterop How to design
            this.Listeners.ForEach(l => gameInterop.Event.RemoveListener(l));
            this.Listeners.Clear();
        }

        #endregion Load and Unload

        #region Update

        private int updateOrder;

        public int UpdateOrder
        {
            get
            {
                return this.updateOrder;
            }

            set
            {
                if (this.updateOrder != value)
                {
                    this.updateOrder = value;
                    if (this.UpdateOrderChanged != null)
                    {
                        this.UpdateOrderChanged(this, EventArgs.Empty);
                    }

                    this.OnUpdateOrderChanged(this, null);
                }
            }
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void UpdateContent(IGameFile gameFile, GameTime gameTime)
        {
        }

        public virtual void UpdateInterop(IGameInterop gameInterop, GameTime gameTime)
        {
        }

        #endregion Update

        #region IDisposable

        public virtual void Dispose()
        {
            this.UnloadInterop(GameInterop);
        }

        #endregion IDisposable
    }
}