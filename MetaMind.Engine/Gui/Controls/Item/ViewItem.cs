namespace MetaMind.Engine.Gui.Controls.Item
{
    using System;
    using Layers;
    using Logic;
    using Microsoft.Xna.Framework;
    using Service;
    using Settings;
    using Views;
    using Visuals;

    public class ViewItem : ViewItemStateControl, IViewItem
    {
        public ViewItem(
            ControlManager manager,
            IView view,
            ItemSettings itemSettings) : base(manager)
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

        ~ViewItem()
        {
            this.Dispose(true);
        }

        #region Direct Dependency

        public IView View { get; protected set; }

        public ItemSettings ItemSettings { get; set; }

        public IViewItemLogic ItemLogic { get; set; }

        public dynamic ItemData { get; set; }

        public IViewItemVisual ItemVisual { get; set; }

        public IViewItemLayer ItemLayer { get; set; }

        #endregion

        #region Events

        public event EventHandler<EventArgs> Selected = delegate { };

        public event EventHandler<EventArgs> Unselected = delegate { };

        public event EventHandler<EventArgs> Swapped = delegate { };

        public event EventHandler<EventArgs> Swapping = delegate { };

        public event EventHandler<EventArgs> Transited = delegate { };

        internal void OnSwapping()
        {
            this[ViewItemState.Item_Is_Swaping] = () => true;
            this[ViewItemState.Item_Is_Swapped] = () => false;

            this.Swapping?.Invoke(this, EventArgs.Empty);
        }

        internal void OnSwapped()
        {
            this[ViewItemState.Item_Is_Swaping] = () => false;
            this[ViewItemState.Item_Is_Swapped] = () => true;

            this.Swapped?.Invoke(this, EventArgs.Empty);
        }

        internal void OnTransited()
        {
            this.Transited?.Invoke(this, EventArgs.Empty);
        }

        internal void OnUnselected()
        {
            this[ViewItemState.Item_Is_Selected] = () => false;

            this.Unselected?.Invoke(this, EventArgs.Empty);
        }

        internal void OnSelected()
        {
            this[ViewItemState.Item_Is_Selected] = () => true;

            this.Selected?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.ItemVisual?.Draw(graphics, time, alpha);
        }

        #endregion

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.ItemLogic? .UpdateInput(input, time);
            this.ItemVisual?.UpdateInput(input, time);
        }

        public override void UpdateView(GameTime gameTime)
        {
            this.ItemLogic?.UpdateView(gameTime);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.ItemLogic? .Update(time);
            this.ItemVisual?.Update(time);
        }

        public override void UpdateBackwardBuffer()
        {
            base.UpdateBackwardBuffer();

            this.ItemLogic? .UpdateBackwardBuffer();
            this.ItemVisual?.UpdateBackwardBuffer();
        }

        #endregion

        #region Layer

        public override void Initialize()
        {
            this.ItemLogic? .Initialize();
            this.ItemVisual?.Initialize();

            // TODO(Critical): Hook the root frame?
            base.Initialize();
        }

        public T GetLayer<T>() where T : class, IViewItemLayer
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

                        this.ItemVisual?.Dispose();
                        this.ItemVisual = null;

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