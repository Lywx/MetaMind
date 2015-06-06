namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;

    using Items;
    using Items.Settings;
    using Layers;
    using Logic;
    using Services;
    using Settings;
    using Visuals;

    public class View : ViewEntity, IView
    {
        private readonly List<IViewItem>[] items = {new List<IViewItem>(), new List<IViewItem>()};

        private int currentBuffer;

        public View(ViewSettings viewSettings, ItemSettings itemSettings, List<IViewItem> items)
        {
            if (viewSettings == null)
            {
                throw new ArgumentNullException("viewSettings");
            }

            if (itemSettings == null)
            {
                throw new ArgumentNullException("itemSettings");
            }

            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            this.ViewSettings = viewSettings;
            this.ViewComponents = new Dictionary<string, object>();

            this.ItemsWrite = items;
            this.ItemSettings = itemSettings;
        }

        #region Dependency

        public IViewLayer ViewLayer { get; set; }

        public Dictionary<string, object> ViewComponents { get; private set; }

        public ViewSettings ViewSettings { get; set; }

        public IViewLogic ViewLogic { get; set; }

        public IViewVisual ViewVisual { get; set; }

        public List<IViewItem> ItemsRead
        {
            get { return this.items[this.currentBuffer]; }
            private set { this.items[this.currentBuffer] = value; }
        }

        public List<IViewItem> ItemsWrite
        {
            get { return this.items[this.NextBuffer()]; }
            set { this.items[this.NextBuffer()] = value; }
        }

        public ItemSettings ItemSettings { get; set; }

        #endregion

        #region Layer

        public T GetLayer<T>() where T : class, IViewLayer
        {
            return this.ViewLayer.Get<T>();
        }

        public void SetupLayer()
        {
            this.ViewLogic .SetupLayer();
            this.ViewVisual.SetupLayer();
        }

        #endregion

        #region Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (this.ViewVisual != null)
            {
                this.ViewVisual.Draw(graphics, time, alpha);
            }
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            if (this.ViewLogic != null)
            {
                this.ViewLogic.Update(time);
            }

            if (this.ViewVisual != null)
            {
                this.ViewVisual.Update(time);
            }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (this.ViewLogic != null)
            {
                this.ViewLogic.UpdateInput(input, time);
            }
        }

        #endregion

        #region Double Buffer

        public override void UpdateForwardBuffer()
        {
            base.UpdateForwardBuffer();

            // Update read buffer updated in lasted loop to use in this loop
            this.ItemsRead = this.ItemsWrite.GetRange(0, this.ItemsWrite.Count);
        }

        public override void UpdateBackwardBuffer()
        {
            base.UpdateBackwardBuffer();

            // Swap buffer
            this.currentBuffer = this.NextBuffer();

            if (this.ViewLogic != null)
            {
                this.ViewLogic.UpdateBackwardBuffer();
            }

            if (this.ViewVisual != null)
            {
                this.ViewVisual.UpdateBackwardBuffer();
            }
        }

        private int NextBuffer()
        {
            return 1 - this.currentBuffer;
        }

        #endregion
    }
}