namespace MetaMind.Session.Guis.Screens
{
    using System;
    using Engine.Core.Backend.Content.Texture;
    using Engine.Core.Entity.Common;
    using Engine.Core.Entity.Control.Images;
    using Engine.Core.Entity.Screens;
    using Primtives2D;

    public class StartScreen : MMScreen
    {
        private MMImage imageLogo;

        private MMImage imagePressAnyButton;

        #region Constructors

        public StartScreen()
        {
            this.TransitionOnTime  = TimeSpan.FromSeconds(2.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        #endregion

        #region Load and Unload

        public override void LoadContent()
        {
            this.imageLogo           = this.GlobalInterop.Asset.Texture["Background_Screen_Start_Logo_1"];
            this.imagePressAnyButton = this.GlobalInterop.Asset.Texture["Background_Screen_Start_PressAnyButton_1"];

            this.Add(new MMImageBox());
            this.Schedule();

            this.Children.Add(new MMLayer(this)
            {
                DrawAction = (graphics, time, alpha) =>
                {
                    this.GlobalGraphics.Renderer.Begin(transformMatrix: this.ViewportAdapter.GetScaleMatrix());
                    this.GlobalGraphics.Renderer.Draw(this.imageLogo, );
                    this.GlobalGraphics.Renderer.End();

                    Primitives2D.FillRectangle(((MMVisualEntity)this).GlobalGraphics.Renderer, this.RenderTargetDestinationRectangle, this.Settings.GetColor());
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