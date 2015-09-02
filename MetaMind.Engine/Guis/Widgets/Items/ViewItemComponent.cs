namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;
    using Layers;
    using Views;
    using Views.Layers;

    public class ViewItemComponent : GameControllableEntity, IViewItemComponent
    {
        #region Constructors and Destructors

        protected ViewItemComponent(IViewItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            this.Item = item;
        }

        ~ViewItemComponent()
        {
            this.Dispose();
        }

        #endregion Constructors and Destructors

        #region Direct Dependency

        public IViewItem Item { get; }

        #endregion 

        #region Indirect Dependency

        public IView View => this.Item.View;

        private IViewLayer ViewLayer => this.View.ViewLayer;

        private IViewItemLayer ItemLayer => this.Item.ItemLayer;

        #endregion

        #region Layer

        public T ItemGetLayer<T>() where T : class, IViewItemLayer
        {
            return this.ItemLayer.Get<T>();
        }

        public T ViewGetLayer<T>() where T : class, IViewLayer
        {
            return this.ViewLayer.Get<T>();
        }

        public virtual void SetupLayer() { }

        #endregion
    }
}