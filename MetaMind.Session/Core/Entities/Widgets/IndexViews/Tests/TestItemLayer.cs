namespace MetaMind.Session.Guis.Widgets.IndexViews.Tests
{
    using Engine.Core.Entity.Control.Item;
    using Engine.Core.Entity.Control.Item.Layers;

    public class TestItemLayer : MMIndexBlockViewVerticalItemLayer
    {
        public TestItemLayer(IMMViewItem item) 
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