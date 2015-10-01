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
    using Service;
    using Settings;
    using Visuals;

    /// <summary>
    /// Abstract view object. View is a container of logic and visual element. 
    /// The design try to enforce component reuse and replacement.
    /// </summary>
    public class View : ViewStateControl, IView
    {
        private readonly List<IViewItem>[] items =
        {
            new List<IViewItem>(),
            new List<IViewItem>(),
        };

        public View(
            ControlManager manager,

            // TODO(Critical): I need to change how I structure settings and structure and component
            ViewSettings viewSettings,
            ItemSettings itemSettings,
            List<IViewItem> items) 
            : base(manager)
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

        public override void LoadContent(IMMEngineInteropService interop)
        {
            if (this.ViewLogic != null)
            {
                this.ViewLogic.Initialize();
                this.ViewLogic.LoadBinding();
            }

            this.ViewVisual?.Initialize();

            base.LoadContent(interop);
        }

        public override void UnloadContent(IMMEngineInteropService interop)
        {
            this.ViewLogic?.UnloadBinding();
            base           .UnloadContent(interop);
        }

        #endregion

        #region Draw

        public override void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            this.ViewVisual?.Draw(graphics, time, alpha);
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            base.Update(time);

            // TODO(Critical): May change update mechanism in view
            this.ViewLogic? .Update(time);
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
             this.ViewLogic?.UpdateInput(input, time);

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