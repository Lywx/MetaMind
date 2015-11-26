namespace MetaMind.Session.Guis.Screens
{
    using System;
    using Engine.Components.Content.Texture;
    using Engine.Components.Graphics;
    using Engine.Entities;
    using Engine.Screens;
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
            this.imageLogo           = this.Interop.Asset.Texture["StartScreen.Meta Mind"];
            this.imagePressAnyButton = this.Interop.Asset.Texture["StartScreen.Press Any Button"];

            this.Children.Add(new MMLayer(this)
            {
                DrawAction = (graphics, time, alpha) =>
                {
                    ((MMVisualEntity)this).Graphics.Renderer.Begin(transformMatrix: this.ViewportAdapter.GetScaleMatrix());
                    ((MMVisualEntity)this).Graphics.Renderer.Draw(this.imageLogo, );
                    ((MMVisualEntity)this).Graphics.Renderer.End();

                    Primitives2D.FillRectangle(((MMVisualEntity)this).Graphics.Renderer, this.RenderTargetDestinationRectangle, this.Settings.GetColor());
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