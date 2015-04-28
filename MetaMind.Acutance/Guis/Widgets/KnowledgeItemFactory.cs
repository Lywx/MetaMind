namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Widgets.Items;

    public class KnowledgeItemFactory : ViewItemFactory2D
    {
        public override dynamic CreateLogicControl(IViewItem item)
        {
            return new KnowledgeItemLogicControl(item);
        }

        public override IItemVisualControl CreateVisualControl(IViewItem item)
        {
            return new KnowledgeItemVisualControl(item);
        }

        public void RemoveData(IViewItem item)
        {
        }
    }
}