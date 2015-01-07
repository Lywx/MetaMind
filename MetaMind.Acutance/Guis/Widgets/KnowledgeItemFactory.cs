namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Widgets.Items;

    public class KnowledgeItemFactory : ViewItemBasicFactory2D
    {
        public override dynamic CreateControl(IViewItem item)
        {
            return new KnowledgeItemControl(item);
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