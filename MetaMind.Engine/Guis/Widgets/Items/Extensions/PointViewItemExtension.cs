namespace MetaMind.Engine.Guis.Widgets.Items.Extensions
{
    public class PointViewItemExtension : ViewItemExtension
    {
        protected PointViewItemExtension(IViewItem item)
            : base(item)
        {
        }

        public new PointV ItemLogic
        {
            get { return this.Item.ItemLogic; }
        }
    }
}