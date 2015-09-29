namespace MetaMind.Unity.Guis.Screens
{
    using System;
    using Engine.Components.Content.Texture;
    using Engine.Screen;
    using Engine.Service;
    using Primtives2D;

    public class StartScreen : GameScreen
    {
        private Image textureLogo;

        private Image texturePressAnyButton;

        #region Constructors

        public StartScreen()
        {
            this.TransitionOnTime = TimeSpan.FromSeconds(2.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        #endregion

        #region Load and Unload

        public override void LoadContent(IGameInteropService interop)
        {
            this.textureLogo           = interop.Asset.Texture["StartScreen.Meta Mind"];
            this.texturePressAnyButton = interop.Asset.Texture["StartScreen.Press Any Button"];

            this.Layers.Add(new GameLayer(this)
            {
                DrawAction = (graphics, time, alpha) =>
                {
                    this.SpriteBatch.Begin(transformMatrix: this.ViewportAdapter.GetScaleMatrix());
                    this.SpriteBatch.Draw(this.textureLogo, );
                    this.SpriteBatch.End();

                    Primitives2D.FillRectangle(this.SpriteBatch, this.RenderTargetDestinationRectangle, this.Settings.GetColor());
                },
                UpdateAction = time =>
                {
                }
            });
        }

        public override void UnloadContent(IGameInteropService interop)
        {
        }

        #endregion Load and Unload

    }
}