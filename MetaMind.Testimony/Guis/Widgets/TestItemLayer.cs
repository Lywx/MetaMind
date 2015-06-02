namespace MetaMind.Testimony.Guis.Widgets
{
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Layers;
    using Engine.Guis.Widgets.Items.Layouts;

    public class TestItemLayer : PointView2DItemLayer
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

        public IViewItemLayout ItemLayout
        {
            get { return this.ItemLogic.ItemLayout; }
        }
    }
}