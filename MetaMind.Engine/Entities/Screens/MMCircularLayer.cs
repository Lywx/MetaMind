namespace MetaMind.Engine.Entities.Screens
{
    using System;
    using Bases;
    using Microsoft.Xna.Framework;

    public class MMCircularLayer : MMLayer, ICircularLayerManager
    {
        private int layerDisplayedIndex;

        public MMCircularLayer(IMMScreen screen)
            : base(screen)
        {
        }

        public MMEntityCollection<IMMLayer> Layers { get; protected set; } = new MMEntityCollection<IMMLayer>();

        public IMMLayer LayerDisplayed => this.Layers[this.layerDisplayedIndex];

        #region Load and Unload

        public override void LoadContent()
        {
            this.Layers.LoadContent();
            base       .LoadContent();
        }

        public override void UnloadContent()
        {
            this.Layers.UnloadContent();
            base       .UnloadContent();
        }

        #endregion

        #region Update and Draw 

        public override void Draw(GameTime time)
        {
            this.Layers.Draw(time);
            base       .Draw(time);
        }

        public override void Update(GameTime time)
        {
            this.Layers.ForEach(layer => layer.Update(time));
            base                              .Update(time);
        }

        public override void UpdateInput(GameTime time)
        {
            this.LayerDisplayed.UpdateInput(time);
            base               .UpdateInput(time);
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

        public void Add(IMMLayer layer)
        {
            this.Layers.Add(layer);
        }

        public void Remove(IMMLayer layer)
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