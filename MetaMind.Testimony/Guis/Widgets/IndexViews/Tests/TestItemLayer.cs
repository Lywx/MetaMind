namespace MetaMind.Testimony.Guis.Widgets.IndexViews.Tests
{
    using Engine.Guis.Widgets.Items;

    public class TestItemLayer : StandardItemLayer
    {
        public TestItemLayer(IViewItem item) 
            : base(item)
        {
        }

        public new TestItemLogic ItemLogic
        {
            get { return (TestItemLogic)base.ItemLogic; }
        }
    }
}