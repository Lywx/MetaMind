﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngine.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    using Components;
    using Services;

    using Microsoft.Xna.Framework;

    public partial class GameEngine : Microsoft.Xna.Framework.Game, IGameEngine
    {
        private IGameGraphics graphics;

        private IGameNumerical numerical;

        private IGameInterop interop;

        private IGameInput input;

        public GameEngine(string content)
        {
            this.Content.RootDirectory = content;
        }

        #region Global Service Provider

        public static IGameService Service { get; private set; }

        #endregion

        #region Property Injection

        public IGameGraphics Graphics
        {
            get
            {
                return this.graphics ?? (this.graphics = new GameNullGraphics());
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (this.graphics != null)
                {
                    throw new InvalidOperationException();
                }

                this.graphics = value;
            }
        }

        public IGameNumerical Numerical
        {
            get
            {
                return this.numerical
                       ?? (this.numerical = new GameEngineNumerical());
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (this.numerical != null)
                {
                    throw new InvalidOperationException();
                }

                this.numerical = value;
            }
        }

        public IGameInterop Interop
        {
            get
            {
                return this.interop
                       ?? (this.interop = new GameNullInterop(this));
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (this.interop != null)
                {
                    throw new InvalidOperationException();
                }

                this.interop = value;
            }
        }

        public IGameInput Input
        {
            get { return this.input ?? (this.input = new GameNullInput(this)); }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (this.input != null)
                {
                    throw new InvalidOperationException();
                }

                this.input = value;
            }
        }

        #endregion

        #region Game

        protected override void Initialize()
        {
            // Service is loaded after GameEngine.Initialize. But it has to 
            // be constructed after Components.
            Service = new GameEngineService(
                new GameEngineGraphicsService(this.Graphics),
                new GameEngineInputService(this.Input),
                new GameEngineInteropService(this.Interop),
                new GameEngineNumericalService(this.Numerical));

            // Graphics has to initialized first
            this.Graphics .Initialize();
            this.Input    .Initialize();
            this.Interop  .Initialize();
            this.Numerical.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            this.UpdateInput(gameTime);
            base.Update(gameTime);
        }

        private void UpdateInput(GameTime gameTime)
        {
            this.Input  .UpdateInput(gameTime);
            this.Interop.UpdateInput(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            this.Interop.OnExiting();
            base        .OnExiting(sender, args);

            this.Dispose(true);
        }

        #endregion


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
                        // Don't set to null, for there is null checking in property 
                        // injection
                        this.Interop?.Dispose();
                        this.interop = null;

                        this.Input?.Dispose();
                        this.input = null;

                        this.Graphics?.Dispose();
                        this.graphics = null;
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

    public partial class GameEngine
    {
        #region Operations

        public void Restart()
        {
            // Save immediately because the Exit is an asynchronous call, 
            // which may not finished before Process.Start() is called
            this.Interop.Save.Save();

            this.Exit();

            using (var p = Process.GetCurrentProcess())
            {
                Process.Start(Assembly.GetEntryAssembly().Location, p.StartInfo.Arguments);
            }
        }

        #endregion
    }
}