namespace MetaMind.Session.Guis.Screens
{
    using System;
    using Engine.Components.Graphics;
    using Engine.Entities;
    using Engine.Screens;
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

        public override void LoadContent()
        {
            this.Layers.Add(new MMLayer(this)
            {
                DrawAction = (graphics, time, alpha) =>
                {
                    ((MMVisualEntity)this).Graphics.Renderer.Begin();
                    Primitives2D.FillRectangle(((MMVisualEntity)this).Graphics.Renderer, this.RenderTargetDestinationRectangle, this.Settings.GetColor());
                    ((MMVisualEntity)this).Graphics.Renderer.End();
                },
                UpdateAction = time =>
                {
                }
            });
        }

        public override void UnloadContent()
        {
        }

        #endregion Load and Unload
    }
}