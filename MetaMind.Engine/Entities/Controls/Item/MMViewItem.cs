namespace MetaMind.Engine.Entities.Controls.Item
{
    using System;
    using Layers;
    using Microsoft.Xna.Framework;
    using Settings;
    using Views;

    public class MMViewItem : MMViewItemStateHolder, IMMViewItem, IMMViewItemInternal
    {
        public MMViewItem(IMMView view, ItemSettings itemSettings)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            this.View = view;

            if (itemSettings == null)
            {
                throw new ArgumentNullException(nameof(itemSettings));
            }

            this.ItemSettings = itemSettings;
        }

        ~MMViewItem()
        {
            this.Dispose(true);
        }

        #region Direct Dependency

        public IMMView View { get; private set; }

        public ItemSettings ItemSettings { get; set; }

        public IMMViewItemController ItemLogic { get; set; }

        public dynamic ItemData { get; set; }

        public IMMViewItemLayer ItemLayer { get; set; }

        #endregion

        #region Events

        public event EventHandler<EventArgs> Selected = delegate { };

        public event EventHandler<EventArgs> Unselected = delegate { };

        public event EventHandler<EventArgs> Swapped = delegate { };

        public event EventHandler<EventArgs> Swapping = delegate { };

        public event EventHandler<EventArgs> Transited = delegate { };

        internal void OnSwapping()
        {
            this[MMViewItemState.Item_Is_Swaping] = () => true;
            this[MMViewItemState.Item_Is_Swapped] = () => false;

            this.Swapping?.Invoke(this, EventArgs.Empty);
        }

        internal void OnSwapped()
        {
            this[MMViewItemState.Item_Is_Swaping] = () => false;
            this[MMViewItemState.Item_Is_Swapped] = () => true;

            this.Swapped?.Invoke(this, EventArgs.Empty);
        }

        internal void OnTransited()
        {
            this.Transited?.Invoke(this, EventArgs.Empty);
        }

        internal void OnUnselected()
        {
            this[MMViewItemState.Item_Is_Selected] = () => false;

            this.Unselected?.Invoke(this, EventArgs.Empty);
        }

        internal void OnSelected()
        {
            this[MMViewItemState.Item_Is_Selected] = () => true;

            this.Selected?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Draw

        public override void Draw(GameTime time)
        {
            this.Renderer?.Draw(time);
        }

        #endregion

        #region Update

        public override void UpdateInput(GameTime time)
        {
            this.ItemLogic?.UpdateInput(time);
            this.Renderer?.UpdateInput(time);
        }

        public override void UpdateView(GameTime time)
        {
            this.ItemLogic?.UpdateView(time);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.ItemLogic? .Update(time);
            this.Renderer?.Update(time);
        }

        #endregion

        #region Buffer

        public void UpdateBackwardBuffer()
        {
            this.ItemLogic?.UpdateBackwardBuffer();
            this.Renderer?.UpdateBackwardBuffer();
        }

        #endregion

        #region Layer

        public override void Initialize()
        {
            this.ItemLogic?.Initialize();
            this.Renderer?.Initialize();

            // TODO(Critical): Hook the root frame?
            base.Initialize();
        }

        public T GetLayer<T>() where T : class, IMMViewItemLayer
        {
            return this.ItemLayer.Get<T>();
        }

        #endregion


        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.IsDisposed)
                    {
                        this.DisposeEvents();

                        this.ItemLogic?.Dispose();
                        this.ItemLogic = null;

                        this.Renderer?.Dispose();
                        this.Renderer = null;

                        this.ItemLayer?.Dispose();
                        this.ItemLayer = null;
                    }

                    this.IsDisposed = true;
                }
            }
            catch
            {
                // Ignored
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        protected virtual void DisposeEvents()
        {
            this.Selected   = null;
            this.Unselected = null;
            this.Swapped    = null;
            this.Swapping   = null;
            this.Transited  = null;
        }

        #endregion
    }
}