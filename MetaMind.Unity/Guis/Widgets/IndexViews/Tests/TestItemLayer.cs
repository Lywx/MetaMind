namespace MetaMind.Unity.Guis.Widgets.IndexViews.Tests
{
    using Engine.Guis.Controls.Items;
    using Engine.Guis.Controls.Items.Layers;

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