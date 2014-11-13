namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.ViewItems;
    using MetaMind.Perseverance.Concepts.MotivationEntries;

    public class MotivationItemFactory : IViewItemFactory
    {
        public dynamic CreateControl(IViewItem item)
        {
            return new MotivationItemControl(item);
        }

        public dynamic CreateData(IViewItem item)
        {
            return Perseverance.Adventure.Motivationlist.Create(item.ViewSettings.Space);
        }

        public IItemGraphics CreateGraphics(IViewItem item)
        {
            return new MotivationItemGraphics(item);
        }

        public void RemoveData(IViewItem item)
        {
            MotivationEntry motivation = item.ItemData;
            
            // remove from source
            Perseverance.Adventure.Motivationlist.Remove(item.ItemData, item.ViewSettings.Space);
            
            // remove sub-tasks
            motivation.Tasks.ForEach(task => Perseverance.Adventure.Tasklist.Remove(task));
        }
    }
}