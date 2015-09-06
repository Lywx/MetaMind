namespace MetaMind.Engine.Guis.Controls.Views
{
    using System;
    using System.Collections.Generic;
    using Items;
    using Items.Settings;
    using Layers;
    using Logic;
    using Microsoft.Xna.Framework;
    using Services;
    using Settings;
    using Visuals;

    public class View : ViewEntity, IView
    {
        private readonly List<IViewItem>[] items =
        {
            new List<IViewItem>(),
            new List<IViewItem>()
        };

        public View(ViewSettings viewSettings, ItemSettings itemSettings, List<IViewItem> items)
        {
            if (viewSettings == null)
            {
                throw new ArgumentNullException(nameof(viewSettings));
            }

            if (itemSettings == null)
            {
                throw new ArgumentNullException(nameof(itemSettings));
            }

            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            this.ViewComponents = new Dictionary<string, object>();

            this.ViewSettings = viewSettings;
            this.ItemSettings = itemSettings;
            this.ItemsWrite   = items;
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

        #endregion

        #region Components

        public T GetComponent<T>(string id) where T : class
        {
            var t = (T)this.ViewGetComponent(id);
            if (t == null)
            {
                throw new InvalidOperationException($"ViewComponents has no child {id} of type {typeof(T).Name}");
            }

            return t;
        }

        private object ViewGetComponent(string id)
        {
            return this.ViewComponents.ContainsKey(id) ? this.ViewComponents[id] : null;
        }

        #endregion

        #region Load and Unload

        public override void LoadContent(IGameInteropService interop)
        {
            if (this.ViewLogic != null)
            {
                this.ViewLogic.SetupLayer();

                this.ViewLogic.LoadBinding();
            }

            this.ViewVisual?.SetupLayer();

            base.LoadContent(interop);
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            this.ViewLogic?.UnloadBinding();

            base.UnloadContent(interop);
        }


        #endregion

        #region Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.ViewVisual?.Draw(graphics, time, alpha);
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.ViewLogic?.Update(time);
            this.ViewVisual?.Update(time);

            foreach (var pair in this.ViewComponents)
            {
                var component  = pair.Value;
                var updateable = component as IOuterUpdateable;
                updateable?.Update(time);
            }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.ViewLogic?.UpdateInput(input, time);

            foreach (var pair in this.ViewComponents)
            {
                var component  = pair.Value;
                var updateable = component as IInputable;
                updateable?.UpdateInput(input, time);
            }
        }

        #endregion

        #region Buffer

        private int currentBuffer;

        public override void UpdateForwardBuffer()
        {
            base.UpdateForwardBuffer();

            // Update read buffer updated in latest input / event / process loop to use in this loop
            this.UpdateItemsReadBuffer();

            this.ViewLogic? .UpdateForwardBuffer();
            this.ViewVisual?.UpdateForwardBuffer();
        }

        public override void UpdateBackwardBuffer()
        {
            base.UpdateBackwardBuffer();

            // Update read buffer updated in latest update loop to use in this loop
            this.UpdateItemsReadBuffer();

            // Swap buffer
            this.currentBuffer = this.NextBuffer();

            this.ViewLogic? .UpdateBackwardBuffer();
            this.ViewVisual?.UpdateBackwardBuffer();
        }

        private void UpdateItemsReadBuffer()
        {
            this.ItemsRead = this.ItemsWrite.GetRange(0, this.ItemsWrite.Count);
        }

        private int NextBuffer()
        {
            return 1 - this.currentBuffer;
        }

        #endregion
    }
}