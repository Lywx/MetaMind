namespace MetaMind.Engine.Guis.Layers
{
    using System.Collections.Generic;
    using Engine;
    using Engine.Screens;
    using Engine.Services;
    using Microsoft.Xna.Framework;

    public class CircularGameLayer : GameLayer, ICircularLayerManager
    {
        private readonly GameControllableEntityCollection<IGameLayer> gameLayers = new GameControllableEntityCollection<IGameLayer>();

        private int gameLayerDisplayedIndex;

        public CircularGameLayer(IGameScreen screen, byte transitionAlpha = byte.MaxValue)
            : base(screen, transitionAlpha)
        {
        }

        public List<IGameLayer> GameLayers
        {
            get { return this.gameLayers; }
        }

        public IGameLayer GameLayerDisplayed
        {
            get { return this.GameLayers[this.gameLayerDisplayedIndex]; }
        }

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
            this.GameLayerDisplayed.Draw(graphics, time, alpha);

            base.Draw(graphics, time, alpha);
        }

        public override void Update(GameTime time)
        {
            this.GameLayers.ForEach(layer => layer.Update(time));

            base.Update(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.GameLayerDisplayed.UpdateInput(input, time);

            base.UpdateInput(input, time);
        }

        public override void UpdateBackwardBuffer()
        {
            this.GameLayerDisplayed.UpdateBackwardBuffer();

            base.UpdateBackwardBuffer();
        }

        public override void UpdateForwardBuffer()
        {
            this.GameLayerDisplayed.UpdateForwardBuffer();

            base.UpdateForwardBuffer();
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

        public void NextLayer()
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

        public void PreviousLayer()
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

        #endregion
    }
}
