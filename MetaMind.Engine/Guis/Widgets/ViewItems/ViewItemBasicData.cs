namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public interface IViewItemBasicData : IViewItemData
    {
    }

    public class ViewItemBasicData : ViewItemComponent, IViewItemBasicData
    {
        private string[] labels = new string[ ( int ) ViewItemLabelType.LabelNum ];
        public ViewItemBasicData( IViewItem item )
            : base( item )
        {
        }

        public string[] Labels
        {
            get { return labels; }
            set { labels = value; }
        }
    }
}