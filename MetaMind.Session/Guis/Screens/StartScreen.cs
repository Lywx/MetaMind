namespace MetaMind.Session.Guis.Screens
{
    using System;
    using Engine.Components.Content.Texture;
    using Engine.Screens;
    using Engine.Services;
    using Primtives2D;

    public class StartScreen : MMScreen
    {
        private Image imageLogo;

        private Image imagePressAnyButton;

        #region Constructors

        public StartScreen()
        {
            this.TransitionOnTime = TimeSpan.FromSeconds(2.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        #endregion

        #region Load and Unload

        public override void LoadContent()
        {
            this.imageLogo           = interop.Asset.Texture["StartScreen.Meta Mind"];
            this.imagePressAnyButton = interop.Asset.Texture["StartScreen.Press Any Button"];
            var s = new CCSprite(new CCTexture2D(this.imageLogo.Resource));

            this.Layers.Add(new MMLayer(this)
            {
                DrawAction = (graphics, time, alpha) =>
                {
                    this.SpriteBatch.Begin(transformMatrix: this.ViewportAdapter.GetScaleMatrix());
                    this.SpriteBatch.Draw(this.imageLogo, );
                    this.SpriteBatch.End();

                    Primitives2D.FillRectangle(this.SpriteBatch, this.RenderTargetDestinationRectangle, this.Settings.GetColor());
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