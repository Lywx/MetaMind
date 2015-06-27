namespace MetaMind.Testimony.Guis.Widgets.IndexViews.Tests
{
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Layers;

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

        public TestItemFrame ItemFrame
        {
            get { return this.ItemLogic.ItemFrame; }
        }
    }
}