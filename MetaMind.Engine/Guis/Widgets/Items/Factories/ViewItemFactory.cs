namespace MetaMind.Engine.Guis.Widgets.Items.Factories
{
    using System;
    using Layers;
    using MetaMind.Engine.Guis.Widgets.Items.Logic;
    using MetaMind.Engine.Guis.Widgets.Items.Visuals;

    public sealed class ViewItemFactory : IViewItemFactory
    {
        public ViewItemFactory(
            Func<IViewItem, IViewItemLayer>  itemLayer,
            Func<IViewItem, IViewItemLogic>  itemLogic,
            Func<IViewItem, IViewItemVisual> itemVisual,
            Func<IViewItem, dynamic>         itemData)
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


            if (itemData == null)
            {
                throw new ArgumentNullException("itemData");
            }

            this.ItemLayer  = itemLayer;
            this.ItemLogic  = itemLogic;
            this.ItemVisual = itemVisual;
            this.ItemData   = itemData;
        }

        private Func<IViewItem, IViewItemLayer> ItemLayer { get; set; }

        private Func<IViewItem, IViewItemLogic> ItemLogic { get; set; }

        private Func<IViewItem, IViewItemVisual> ItemVisual { get; set; }

        private Func<IViewItem, dynamic> ItemData { get; set; }

        public dynamic CreateData(IViewItem item)
        {
            return this.ItemData(item);
        }

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