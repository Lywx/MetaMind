namespace MetaMind.Session.Guis.Screens
{
    using System;
    using Engine.Screen;
    using Engine.Services;
    using Primtives2D;

    public class BackgroundScreen : MMScreen
    {
        #region Constructors

        public BackgroundScreen(BackgroundScreenSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            this.Settings = settings;

            this.TransitionOnTime  = TimeSpan.FromSeconds(3.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        #endregion Constructors

        #region Static Properties 

        public BackgroundScreenSettings Settings { get; set; }

        #endregion

        #region Load and Unload

        public override void LoadContent(IMMEngineInteropService interop)
        {
            this.Layers.Add(new MMLayer(this)
            {
                DrawAction = (graphics, time, alpha) =>
                {
                    this.SpriteBatch.Begin();
                    Primitives2D.FillRectangle(this.SpriteBatch, this.RenderTargetDestinationRectangle, this.Settings.GetColor());
                    this.SpriteBatch.End();
                },
                UpdateAction = time =>
                {
                }
            });
        }

        public override void UnloadContent(IMMEngineInteropService interop)
        {
        }

        #endregion Load and Unload
    }
}