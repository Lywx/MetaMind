// --------------------------------------------------------------------------------------------------------------------
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

    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Console;
    using MetaMind.Engine.Guis.Console.Commands;
    using MetaMind.Engine.Services;

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
                if (this.graphics == null)
                {
                    this.graphics = new GameNullGraphics();
                }

                return this.graphics;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
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
                if (this.numerical == null)
                {
                    this.numerical = new GameEngineNumerical();
                }

                return this.numerical;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
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
                if (this.interop == null)
                {
                    this.interop = new GameNullInterop();
                }

                return this.interop;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
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
            get
            {
                if (this.input == null)
                {
                    this.input = new GameNullInput();
                }

                return this.input;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
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
        }

        #endregion
    }

    public partial class GameEngine
    {
        #region Operations

        public void Restart()
        {
            this.Exit();

            using (var p = Process.GetCurrentProcess())
            {
                Process.Start(Assembly.GetEntryAssembly().Location, p.StartInfo.Arguments);
            }
        }

        #endregion
    }
}