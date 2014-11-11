namespace MetaMind.Engine.Guis.Elements.ViewItems
{
    using MetaMind.Engine.Guis.Elements.Items;

    public class ViewItemBasicFactory1D : IViewItemFactory
    {
        public dynamic CreateControl( IViewItem item )
        {
            return new ViewItemControl1D( item );
        }

        public dynamic CreateData( IViewItem item )
        {
            return new ViewItemData();
        }

        public IItemGraphics CreateGraphics( IViewItem item )
        {
            return new ViewItemBasicGraphics( item );
        }
    }
}