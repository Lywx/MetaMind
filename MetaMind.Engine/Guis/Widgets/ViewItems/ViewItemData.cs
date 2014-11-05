namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemData : ViewItemComponent
    {
        public string Name;
        public ViewItemData( IViewItem item )
            : base( item )
        {
            Name = string.Empty;
        }

    }
}