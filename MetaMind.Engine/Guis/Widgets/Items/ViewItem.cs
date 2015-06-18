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
                throw new ArgumentNullException("view");
            }

            if (itemSettings == null)
            {
                throw new ArgumentNullException("itemSettings");
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
            
            if (this.Swapping != null)
            {
                this.Swapping(this, EventArgs.Empty);
            }
        }

        internal void OnSwapped()
        {
            this[ItemState.Item_Is_Swaping] = () => false;
            this[ItemState.Item_Is_Swapped] = () => true;

            if (this.Swapped != null)
            {
                this.Swapped(this, EventArgs.Empty);
            }
        }

        internal void OnTransited()
        {
            if (this.Transited != null)
            {
                this.Transited(this, EventArgs.Empty);
            }
        }

        internal void OnUnselected()
        {
            this[ItemState.Item_Is_Selected] = () => false;

            if (this.Unselected != null)
            {
                this.Unselected(this, EventArgs.Empty);
            }
        }

        internal void OnSelected()
        {
            this[ItemState.Item_Is_Selected] = () => true;

            if (this.Selected != null)
            {
                this.Selected(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (this.ItemVisual != null)
            {
                this.ItemVisual.Draw(graphics, time, alpha);
            }
        }

        #endregion

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (this.ItemLogic != null)
            {
                this.ItemLogic.UpdateInput(input, time);
            }

            if (this.ItemVisual != null)
            {
                this.ItemVisual.UpdateInput(input, time);
            }
        }

        public override void UpdateView(GameTime gameTime)
        {
            if (this.ItemLogic != null)
            {
                this.ItemLogic.UpdateView(gameTime);
            }
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            if (this.ItemLogic != null)
            {
                this.ItemLogic.Update(time);
            }

            if (this.ItemVisual != null)
            {
                this.ItemVisual.Update(time);
            }
        }

        public override void UpdateBackwardBuffer()
        {
            base.UpdateBackwardBuffer();

            if (this.ItemLogic != null)
            {
                this.ItemLogic.UpdateBackwardBuffer();
            }

            if (this.ItemVisual != null)
            {
                this.ItemVisual.UpdateBackwardBuffer();
            }
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

        public override void Dispose()
        {
            this.Selected   = null;
            this.Unselected = null;
            this.Swapped    = null;
            this.Swapping   = null;
            this.Transited  = null;

            if (this.ItemLogic != null)
            {
                this.ItemLogic.Dispose();
            }

            if (this.ItemVisual != null)
            {
                this.ItemVisual.Dispose();
            }

            if (this.ItemLayer != null)
            {
                this.ItemLayer.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}