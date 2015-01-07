namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;

    public class MotivationItemFactory : IViewItemFactory
    {
        public dynamic CreateControl(IViewItem item)
        {
            return new MotivationItemControl(item);
        }

        public dynamic CreateData(IViewItem item)
        {
            return Perseverance.Session.Motivationlist.Create(item.ViewSettings.Space);
        }

        public IItemGraphics CreateGraphics(IViewItem item)
        {
            return new MotivationItemGraphics(item);
        }

        public void RemoveData(IViewItem item)
        {
            Perseverance.Session.Motivationlist.Remove(item.ItemData, item.ViewSettings.Space);
        }
    }
}