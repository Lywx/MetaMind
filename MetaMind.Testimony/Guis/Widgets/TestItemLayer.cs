namespace MetaMind.Testimony.Guis.Widgets
{
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Layers;

    public class TestItemLayer : BlockViewVerticalItemLayer
    {
        public TestItemLayer(IViewItem item) : base(item)
        {
        }

        public new TestItemSettings ItemSettings
        {
            get { return (TestItemSettings)base.ItemSettings; }
        }

        public new TestItemLogic ItemLogic
        {
            get { return (TestItemLogic)base.ItemLogic; }
        }

        public TestItemFrame ItemFrame
        {
            get { return this.ItemLogic.ItemFrame; }
        }
    }
}