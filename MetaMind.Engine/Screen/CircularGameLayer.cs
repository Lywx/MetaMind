﻿namespace MetaMind.Engine.Screen
{
    using System;
    using Engine;
    using Microsoft.Xna.Framework;
    using Service;

    public class CircularGameLayer : GameLayer, ICircularLayerManager
    {
        private int layerDisplayedIndex;

        public CircularGameLayer(IGameScreen screen)
            : base(screen)
        {
        }

        public GameEntityCollection<IGameLayer> Layers { get; protected set; } = new GameEntityCollection<IGameLayer>();

        public IGameLayer LayerDisplayed => this.Layers[this.layerDisplayedIndex];

        #region Load and Unload

        public override void LoadContent(IGameInteropService interop)
        {
            this.Layers.LoadContent(interop);
            base       .LoadContent(interop);
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            this.Layers.UnloadContent(interop);
            base       .UnloadContent(interop);
        }

        #endregion

        #region Update and Draw 

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Layers.Draw(graphics, time, alpha);
            base       .Draw(graphics, time, alpha);
        }

        public override void UpdateTransition(GameTime time)
        {
            this.Layers.ForEach(layer => layer.UpdateTransition(time));
            base                              .UpdateTransition(time);
        }

        public override void Update(GameTime time)
        {
            this.Layers.ForEach(layer => layer.Update(time));
            base                              .Update(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.LayerDisplayed.UpdateInput(input, time);
            base               .UpdateInput(input, time);
        }

        public override void UpdateBackwardBuffer()
        {
            this.Layers.UpdateBackwardBuffer();
            base       .UpdateBackwardBuffer();
        }

        public override void UpdateForwardBuffer()
        {
            this.Layers.UpdateForwardBuffer();
            base       .UpdateForwardBuffer();
        }

        #endregion

        #region Layer Operations

        public void Add(IGameLayer layer)
        {
            this.Layers.Add(layer);
        }

        public void Remove(IGameLayer layer)
        {
            this.Layers.Remove(layer);
        }

        public void Next()
        {
            this.Next(TimeSpan.FromSeconds(1));
        }

        public void Next(TimeSpan time)
        {
            var before = this.LayerDisplayed;
            before.FadeOut(time);

            this.NextLayer();

            var after = this.LayerDisplayed;
            after.FadeIn(time);
        }

        private void NextLayer()
        {
            if (this.layerDisplayedIndex < this.Layers.Count - 1)
            {
                ++this.layerDisplayedIndex;
            }
            else
            {
                this.layerDisplayedIndex = 0;
            }
        }

        public void Previous()
        {
            this.Previous(TimeSpan.FromSeconds(1));
        }

        public void Previous(TimeSpan time)
        {
            var before = this.LayerDisplayed;
            before.FadeOut(time);

            this.PreviousLayer();

            var after = this.LayerDisplayed;
            after.FadeIn(time);
        }

        private void PreviousLayer()
        {
            if (this.layerDisplayedIndex > 0)
            {
                --this.layerDisplayedIndex;
            }
            else
            {
                this.layerDisplayedIndex = this.Layers.Count - 1;
            }
        }

        #endregion
    }
}
