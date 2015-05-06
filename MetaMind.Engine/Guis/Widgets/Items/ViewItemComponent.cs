namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items.Layers;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;

    public abstract class ViewItemComponent : GameControllableEntity, IViewItemComponent
    {
        #region Constructors and Destructors

        protected ViewItemComponent(IViewItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this.Item = item;
        }

        ~ViewItemComponent()
        {
            this.Dispose();
        }

        #endregion Constructors and Destructors

        #region Direct Dependency

        public IViewItem Item { get; private set; }

        #endregion 

        #region Indirect Dependency

        public IView View
        {
            get
            {
                return this.Item.View;
            } 
        }

        private IViewLayer ViewLayer
        {
            get
            {
                return this.View.ViewLayer;
            }
        }

        private IViewItemLayer ItemLayer
        {
            get
            {
                return this.Item.ItemLayer;
            }
        }

        public T ItemGetLayer<T>() where T : class, IViewItemLayer 
        {
            return this.ItemLayer.Get<T>();
        }

        public T ViewGetLayer<T>() where T : class, IViewLayer
        {
            return this.ViewLayer.Get<T>();
        }

        #endregion 
    }
}