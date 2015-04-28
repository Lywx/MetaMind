namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Widgets.Items;

    public class KnowledgeItemFactory : ViewItemFactory2D
    {
        public override dynamic CreateControl(IViewItem item)
        {
            return new KnowledgeItemControl(item);
        }

        public override IItemVisualControl CreateGraphics(IViewItem item)
        {
            return new KnowledgeItemVisualControl(item);
        }

        public void RemoveData(IViewItem item)
        {
        }
    }
}