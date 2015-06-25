namespace MetaMind.Engine.Guis.Widgets.Views
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

    public partial class View : ViewEntity, IView
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
                throw new ArgumentNullException("viewSettings");
            }

            this.ViewSettings = viewSettings;

            this.ViewComponents = new Dictionary<string, object>();

            if (itemSettings == null)
            {
                throw new ArgumentNullException("itemSettings");
            }

            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            this.ItemSettings = itemSettings;
            this.ItemsWrite = items;
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
    }

    public partial class View 
    {
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
                throw new InvalidOperationException(string.Format("ViewComponents has no child {0} of type {1}", id, typeof(T).Name));
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

            if (this.ViewVisual != null)
            {
                this.ViewVisual.SetupLayer();
            }

            base.LoadContent(interop);
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            this.ViewLogic.UnloadBinding();

            base.UnloadContent(interop);
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
            base.Update(time);

            if (this.ViewLogic != null)
            {
                this.ViewLogic.Update(time);
            }

            if (this.ViewVisual != null)
            {
                this.ViewVisual.Update(time);
            }

            foreach (var pair in this.ViewComponents)
            {
                var component  = pair.Value;
                var updateable = component as IOuterUpdateable;

                if (updateable != null)
                {
                    updateable.Update(time);
                }
            }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (this.ViewLogic != null)
            {
                this.ViewLogic.UpdateInput(input, time);
            }

            foreach (var pair in this.ViewComponents)
            {
                var component  = pair.Value;
                var updateable = component as IInputable;

                if (updateable != null)
                {
                    updateable.UpdateInput(input, time);
                }
            }
        }

        #endregion

        #region Buffer

        private int currentBuffer;

        public override void UpdateForwardBuffer()
        {
            base.UpdateForwardBuffer();

            // Update read buffer updated in lastest input / event / process loop to use in this loop
            this.UpdateItemsReadBuffer();

            if (this.ViewLogic != null)
            {
                this.ViewLogic.UpdateForwardBuffer();
            }

            if (this.ViewVisual != null)
            {
                this.ViewVisual.UpdateForwardBuffer();
            }
        }

        public override void UpdateBackwardBuffer()
        {
            base.UpdateBackwardBuffer();

            // Update read buffer updated in lastest update loop to use in this loop
            this.UpdateItemsReadBuffer();

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