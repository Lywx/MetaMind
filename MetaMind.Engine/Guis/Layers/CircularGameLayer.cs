namespace MetaMind.Engine.Guis.Layers
{
    using System;
    using Engine;
    using Screens;
    using Services;
    using Microsoft.Xna.Framework;

    public class CircularGameLayer : GameLayer, ICircularLayerManager
    {
        private readonly GameControllableEntityCollection<IGameLayer> gameLayers = new GameControllableEntityCollection<IGameLayer>();

        private int gameLayerDisplayedIndex;

        public CircularGameLayer(IGameScreen screen, byte transitionAlpha = byte.MaxValue)
            : base(screen, transitionAlpha)
        {
        }

        public GameControllableEntityCollection<IGameLayer> GameLayers => this.gameLayers;

        public IGameLayer GameLayerDisplayed => this.GameLayers[this.gameLayerDisplayedIndex];

        #region Load and Unload

        public override void LoadContent(IGameInteropService interop)
        {
            this.gameLayers.LoadContent(interop);

            base.LoadContent(interop);
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            this.gameLayers.UnloadContent(interop);

            base.UnloadContent(interop);
        }

        #endregion

        #region Update and Draw 

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.GameLayers.Draw(graphics, time, alpha);

            base.Draw(graphics, time, alpha);
        }

        public override void UpdateTransition(GameTime time)
        {
            this.GameLayers.ForEach(layer => layer.UpdateTransition(time));
            base                                  .UpdateTransition(time);
        }

        public override void Update(GameTime time)
        {
            this.GameLayers.ForEach(layer => layer.Update(time));
            base                                  .Update(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.GameLayerDisplayed.UpdateInput(input, time);
            base                   .UpdateInput(input, time);
        }

        public override void UpdateBackwardBuffer()
        {
            this.GameLayers.UpdateBackwardBuffer();
            base           .UpdateBackwardBuffer();
        }

        public override void UpdateForwardBuffer()
        {
            this.GameLayers.UpdateForwardBuffer();
            base           .UpdateForwardBuffer();
        }

        #endregion

        #region Layer Operations

        public void Add(IGameLayer layer)
        {
            this.GameLayers.Add(layer);
        }

        public void Remove(IGameLayer layer)
        {
            this.GameLayers.Remove(layer);
        }

        public void Next()
        {
            this.Next(TimeSpan.FromSeconds(1));
        }

        public void Next(TimeSpan time)
        {
            var before = this.GameLayerDisplayed;
            before.FadeOut(time);

            this.NextLayer();

            var after = this.GameLayerDisplayed;
            after.FadeIn(time);
        }

        private void NextLayer()
        {
            if (this.gameLayerDisplayedIndex < this.GameLayers.Count - 1)
            {
                ++this.gameLayerDisplayedIndex;
            }
            else
            {
                this.gameLayerDisplayedIndex = 0;
            }
        }

        public void Previous()
        {
            this.Previous(TimeSpan.FromSeconds(1));
        }

        public void Previous(TimeSpan time)
        {
            var before = this.GameLayerDisplayed;
            before.FadeOut(time);

            this.PreviousLayer();

            var after = this.GameLayerDisplayed;
            after.FadeIn(time);
        }

        private void PreviousLayer()
        {
            if (this.gameLayerDisplayedIndex > 0)
            {
                --this.gameLayerDisplayedIndex;
            }
            else
            {
                this.gameLayerDisplayedIndex = this.GameLayers.Count - 1;
            }
        }

        //public void Transition()
        //{
        //    this.GameLayerDisplayed.FadeIn();
        //}

        #endregion
    }
}
