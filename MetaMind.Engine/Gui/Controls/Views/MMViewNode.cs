namespace MetaMind.Engine.Gui.Controls.Views
{
    using System;
    using System.Collections.Generic;
    using Entities;
    using Item;
    using Item.Settings;
    using Layers;
    using Logic;
    using Microsoft.Xna.Framework;
    using Services;
    using Settings;
    using Visuals;

    /// <summary>
    /// Abstract view object. View is a container of logic and visual element. 
    /// The design try to enforce component reuse and replacement.
    /// </summary>
    public class MMViewNode : MMViewNodeState, IMMViewNode, IMMViewNodeInternal 
    {
        private readonly List<IViewItem>[] items =
        {
            new List<IViewItem>(),
            new List<IViewItem>(),
        };

        public MMViewNode(
            ViewSettings viewSettings,
            ItemSettings itemSettings,
            List<IViewItem> items) 
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

        public IMMViewController ViewController { get; set; }

        public IMMViewNodeVisual ViewVisual { get; set; }

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

        public List<IViewItem> Items => this.ItemsRead;

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

        public override void LoadContent(IMMEngineInteropService interop)
        {
            if (this.ViewController != null)
            {
                this.ViewController.Initialize();
                this.ViewController.LoadBinding();
            }

            this.ViewVisual?.Initialize();

            base.LoadContent(interop);
        }

        public override void UnloadContent(IMMEngineInteropService interop)
        {
            this.ViewController?.UnloadBinding();
            base           .UnloadContent(interop);
        }

        #endregion

        #region Draw

        public override void Draw(GameTime time)
        {
            this.ViewVisual?.Draw(time);
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            base.Update(time);

            // TODO(Critical): May change update mechanism in view
            this.ViewController? .Update(time);
            this.ViewVisual?.Update(time);

            foreach (var pair in this.ViewComponents)
            {
                var component  = pair.Value;
                var updateable = component as IMMUpdateable;
                updateable?.Update(time);
            }
        }

        public override void UpdateInput(IMMEngineInputService input, GameTime time)
        {
             this.ViewController?.UpdateInput(input, time);

            foreach (var pair in this.ViewComponents)
            {
                var component  = pair.Value;
                var updateable = component as IMMInputable;
                updateable?.UpdateInput(input, time);
            }
        }

        #endregion

        #region Buffer

        private int currentBuffer;

        public void UpdateForwardBuffer()
        {
            // Update read buffer updated in latest input / event / process loop to use in this loop
            this.UpdateItemsReadBuffer();

            this.ViewController? .UpdateForwardBuffer();
            this.ViewVisual?.UpdateForwardBuffer();
        }

        public void UpdateBackwardBuffer()
        {
            // Update read buffer updated in latest update loop to use in this loop
            this.UpdateItemsReadBuffer();

            // Swap buffer
            this.currentBuffer = this.NextBuffer();

            this.ViewController? .UpdateBackwardBuffer();
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