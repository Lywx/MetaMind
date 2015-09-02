namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    using Layers;
    using Logic;
    using Services;
    using Settings;
    using Views;
    using Visuals;

    using Microsoft.Xna.Framework;

    public class ViewItem : ItemEntity, IViewItem
    {
        public ViewItem(IView view, ItemSettings itemSettings)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            if (itemSettings == null)
            {
                throw new ArgumentNullException(nameof(itemSettings));
            }

            this.View         = view;
            this.ItemSettings = itemSettings;
        }

        ~ViewItem()
        {
            this.Dispose();
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

        public event EventHandler<EventArgs> Selected;

        public event EventHandler<EventArgs> Unselected;

        public event EventHandler<EventArgs> Swapped;

        public event EventHandler<EventArgs> Swapping;

        public event EventHandler<EventArgs> Transited;

        internal void OnSwapping()
        {
            this[ItemState.Item_Is_Swaping] = () => true;
            this[ItemState.Item_Is_Swapped] = () => false;

            this.Swapping?.Invoke(this, EventArgs.Empty);
        }

        internal void OnSwapped()
        {
            this[ItemState.Item_Is_Swaping] = () => false;
            this[ItemState.Item_Is_Swapped] = () => true;

            this.Swapped?.Invoke(this, EventArgs.Empty);
        }

        internal void OnTransited()
        {
            this.Transited?.Invoke(this, EventArgs.Empty);
        }

        internal void OnUnselected()
        {
            this[ItemState.Item_Is_Selected] = () => false;

            this.Unselected?.Invoke(this, EventArgs.Empty);
        }

        internal void OnSelected()
        {
            this[ItemState.Item_Is_Selected] = () => true;

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
            this.ItemLogic?.UpdateInput(input, time);

            this.ItemVisual?.UpdateInput(input, time);
        }

        public override void UpdateView(GameTime gameTime)
        {
            this.ItemLogic?.UpdateView(gameTime);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.ItemLogic ?.Update(time);
            this.ItemVisual?.Update(time);
        }

        public override void UpdateBackwardBuffer()
        {
            base.UpdateBackwardBuffer();

            this.ItemLogic ?.UpdateBackwardBuffer();
            this.ItemVisual?.UpdateBackwardBuffer();
        }

        #endregion

        #region Layer

        public void SetupLayer()
        {
            this.ItemLogic .SetupLayer();
            this.ItemVisual.SetupLayer();
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
                        this.Selected   = null;
                        this.Unselected = null;
                        this.Swapped    = null;
                        this.Swapping   = null;
                        this.Transited  = null;

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

        #endregion
    }
}