namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Widgets.Items;

    public class KnowledgeItemFactory : ViewItemFactory2D
    {
        public override dynamic CreateLogicControl(IViewItem item)
        {
            return new KnowledgeItemLogic(item);
        }

        public override IItemVisual CreateVisualControl(IViewItem item)
        {
            return new KnowledgeItemVisual(item);
        }

        public void RemoveData(IViewItem item)
        {
        }
    }
}