namespace MetaMind.Engine.Gui.Control.Item.Factories
{
    using System;
    using Layers;
    using Logic;
    using Visuals;

    public sealed class ViewItemFactory : IViewItemFactory
    {
        public ViewItemFactory(
            Func<IViewItem, IViewItemLayer>  itemLayer,
            Func<IViewItem, IViewItemLogic>  itemLogic,
            Func<IViewItem, IViewItemVisual> itemVisual)
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

        private Func<IViewItem, IViewItemLayer> ItemLayer { get; set; }

        private Func<IViewItem, IViewItemLogic> ItemLogic { get; set; }

        private Func<IViewItem, IViewItemVisual> ItemVisual { get; set; }

        public IViewItemLogic CreateLogic(IViewItem item)
        {
            return this.ItemLogic(item);
        }

        public IViewItemVisual CreateVisual(IViewItem item)
        {
            return this.ItemVisual(item);
        }

        public IViewItemLayer CreateLayer(IViewItem item)
        {
            return this.ItemLayer(item);
        }
    }
}