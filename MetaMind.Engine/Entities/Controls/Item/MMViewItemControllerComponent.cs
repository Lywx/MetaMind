namespace MetaMind.Engine.Entities.Controls.Item
{
    using System;
    using Layers;
    using Views;
    using Views.Layers;

    public class MMViewItemControllerComponent : MMControlComponent, IMMViewItemControllerComponent
    {
        #region Constructors and Finalizer

        protected MMViewItemControllerComponent(IMMViewItem item) : base()
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            this.Item = item;
        }

        ~MMViewItemControllerComponent()
        {
            this.Dispose();
        }

        #endregion 

        #region Direct Dependency

        public IMMViewItem Item { get; }

        #endregion 

        #region Indirect Dependency

        public IMMView View => this.Item.View;

        private IMMViewLayer ViewLayer => this.View.ViewLayer;

        private IMMViewItemLayer ItemLayer => this.Item.ItemLayer;

        #endregion

        #region Layer

        public T GetItemLayer<T>() where T : class, IMMViewItemLayer
        {
            return this.ItemLayer.Get<T>();
        }

        public T GetViewLayer<T>() where T : class, IMMViewLayer
        {
            return this.ViewLayer.Get<T>();
        }

        #endregion
    }
}