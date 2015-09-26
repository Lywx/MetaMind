namespace MetaMind.Engine.Gui.Controls.Item
{
    using System;
    using Layers;
    using Views;
    using Views.Layers;

    public class ViewItemComponent : GameControllableEntity, IViewItemComponent
    {
        #region Constructors and Finalizer

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

        #endregion 

        #region Direct Dependency

        public IViewItem Item { get; }

        #endregion 

        #region Indirect Dependency

        public IView View => this.Item.View;

        private IViewLayer ViewLayer => this.View.ViewLayer;

        private IViewItemLayer ItemLayer => this.Item.ItemLayer;

        #endregion

        #region Initialization

        public virtual void Initialize()
        {
            
        }

        #endregion

        #region Layer

        public T GetItemLayer<T>() where T : class, IViewItemLayer
        {
            return this.ItemLayer.Get<T>();
        }

        public T GetViewLayer<T>() where T : class, IViewLayer
        {
            return this.ViewLayer.Get<T>();
        }

        #endregion
    }
}