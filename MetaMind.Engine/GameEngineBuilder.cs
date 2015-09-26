namespace MetaMind.Engine
{
    using System;
    using Components;
    using Components.Graphics;
    using Components.Interop;
    using Console;
    using Microsoft.Xna.Framework;

    public class GameEngineBuilder : IGameEngineBuilder
    {
        #region Constructor and Finalizer

        public GameEngineBuilder()
        {

        }

        public GameEngineBuilder(IGameEngineConfigurer configurer)
        {
            if (configurer == null)
            {
                throw new ArgumentNullException(nameof(configurer));
            }

            this.Configurer = configurer;
        }

        #endregion

        private GameEngine Engine { get; set; }

        private IGameGraphics Graphics => this.Engine.Graphics;

        private IGameEngineConfigurer Configurer { get; }

        #region Operations

        public GameEngine Create()
        {
            this.Engine = new GameEngine();

            this.BeginSetup();
            this.Setup();
            this.EndSetup();

            return this.Engine;
        }

        /// <summary>
        /// Prepare necessary setup information.
        /// </summary>
        private void BeginSetup()
        {
        }

        /// <summary>
        /// Setup. After this method, the engine is ready to work.
        /// </summary>
        private void Setup()
        {
            this.Engine.Graphics = new GameEngineGraphics(this.Engine, new GraphicsSettings());
            this.Engine.Interop = new GameEngineInterop(this.Engine,
                new ScreenManager(this.Engine, new ScreenSettings(), this.Graphics.SpriteBatch)
                {
                    UpdateOrder = 5
                },
                new GameConsole(
                    new GameConsoleSettings
                    {
                        // TODO(Critical): Font is not loaded
                        Height = this.Graphics.Settings.Height - 50,
                        BackgroundColor = Color.Multiply(new Color(0, 0, 0, 256), 0.25f),
                        PastErrorColor = Color.Red,
                        PastDebugColor = Color.Yellow,
                    },
                    this.Engine,
                    this.Graphics.SpriteBatch,
                    this.Graphics.Renderer));

            this.Engine.Input     = new GameEngineInput(this.Engine);
            this.Engine.Numerical = new GameEngineNumerical();
        }

        /// <summary>
        /// Do extra work to tune engine.
        /// </summary>
        private void EndSetup()
        {
            this.Configurer?.Configure(this.Engine);
        }

        #endregion
    }
}