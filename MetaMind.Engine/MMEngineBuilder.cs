namespace MetaMind.Engine
{
    using System;
    using Components;
    using Components.Graphics;
    using Components.Interop;
    using Microsoft.Xna.Framework;
    using Service.Console;

    public class MMEngineBuilder : IMMEngineBuilder
    {
        #region Constructor and Finalizer

        public MMEngineBuilder()
        {

        }

        public MMEngineBuilder(IMMEngineConfigurer configurer)
        {
            if (configurer == null)
            {
                throw new ArgumentNullException(nameof(configurer));
            }

            this.Configurer = configurer;
        }

        #endregion

        private MMEngine Engine { get; set; }

        private IMMEngineGraphics Graphics => this.Engine.Graphics;

        private IMMEngineConfigurer Configurer { get; }

        #region Operations

        public MMEngine Create()
        {
            this.Engine = new MMEngine();

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
            this.Engine.Graphics = new MMEngineGraphics(this.Engine, new GraphicsSettings());
            this.Engine.Interop = new MMEngineInterop(this.Engine,
                new MMScreenDirector(this.Engine, new MMScreenSettings(), this.Graphics.SpriteBatch)
                {
                    UpdateOrder = 5
                },
                new MMConsole(
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

            this.Engine.Input     = new MMEngineInput(this.Engine);
            this.Engine.Numerical = new MMEngineNumerical();
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