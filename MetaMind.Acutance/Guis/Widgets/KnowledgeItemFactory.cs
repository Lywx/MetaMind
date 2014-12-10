namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.ViewItems;

    public class KnowledgeItemFactory : ViewItemBasicFactory2D
    {
        public override dynamic CreateControl(IViewItem item)
        {
            return new KnowledgeItemControl(item);
        }

        public override dynamic CreateData(IViewItem item)
        {
            return new KnowledgeEntry();
        }

        public override IItemGraphics CreateGraphics(IViewItem item)
        {
            return new KnowledgeItemGraphics(item);
        }

        public void RemoveData(IViewItem item)
        {
        }
    }
}