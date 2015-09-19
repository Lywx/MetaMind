namespace MetaMind.Unity.Guis.Widgets.IndexViews.Tests
{
    using Engine.Gui.Control.Item;
    using Engine.Gui.Control.Item.Layers;

    public class TestItemLayer : IndexBlockViewVerticalItemLayer
    {
        public TestItemLayer(IViewItem item) 
            : base(item)
        {
        }

        public new TestItemLogic ItemLogic
        {
            get { return (TestItemLogic)base.ItemLogic; }
        }

        public new TestItemSettings ItemSettings
        {
            get { return (TestItemSettings)base.ItemSettings; }
        }

        public TestItemFrameController ItemFrame
        {
            get { return this.ItemLogic.ItemFrame; }
        }
    }
}