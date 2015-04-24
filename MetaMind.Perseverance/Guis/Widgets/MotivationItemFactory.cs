namespace MetaMind.Runtime.Guis.Widgets
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
            var s = Runtime.SessionData;
            return s.Motivation.Create();
        }

        public IItemGraphics CreateGraphics(IViewItem item)
        {
            return new MotivationItemGraphics(item);
        }

        public void RemoveData(IViewItem item)
        {
            Runtime.Session.Motivation.Remove(item.ItemData, item.ViewSettings.Space);
        }
    }
}