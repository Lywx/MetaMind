namespace MetaMind.Engine.Core.Entity.Control.Item.Factories
{
    using System;
    using Layers;

    public sealed class ViewItemFactory : IViewItemFactory
    {
        public ViewItemFactory(
            Func<IMMViewItem, IMMViewItemLayer>  itemLayer,
            Func<IMMViewItem, IMMViewItemController>  itemLogic,
            Func<IMMViewItem, IMMViewItemRendererComponent> itemVisual)
        {
            if (itemLayer == null)
            {
                throw new ArgumentNullException("itemLayer");
            }

            if (itemLogic == null)
            {
                throw new ArgumentNullException("itemLogic");
            }

            if (itemVisual == null)
            {
                throw new ArgumentNullException("itemVisual");
            }

            this.ItemLayer  = itemLayer;
            this.ItemLogic  = itemLogic;
            this.ItemVisual = itemVisual;
        }

        private Func<IMMViewItem, IMMViewItemLayer> ItemLayer { get; set; }

        private Func<IMMViewItem, IMMViewItemController> ItemLogic { get; set; }

        private Func<IMMViewItem, IMMViewItemRendererComponent> ItemVisual { get; set; }

        public IMMViewItemController CreateController(IMMViewItem item)
        {
            return this.ItemLogic(item);
        }

        public IMMViewItemRendererComponent CreateRenderer(IMMViewItem item)
        {
            return this.ItemVisual(item);
        }

        public IMMViewItemLayer CreateLayer(IMMViewItem item)
        {
            return this.ItemLayer(item);
        }
    }
}