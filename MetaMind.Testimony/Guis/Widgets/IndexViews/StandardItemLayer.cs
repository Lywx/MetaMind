namespace MetaMind.Testimony.Guis.Widgets.IndexViews
{
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Layers;

    public class StandardItemLayer : IndexBlockViewVerticalItemLayer
    {
        public StandardItemLayer(IViewItem item) : base(item)
        {
        }

        public new StandardIndexItemSettings ItemSettings
        {
            get { return (StandardIndexItemSettings)base.ItemSettings; }
        }

        public StandardItemFrame ItemFrame
        {
            get { return (dynamic)this.ItemLogic.ItemFrame; }
        }
    }
}