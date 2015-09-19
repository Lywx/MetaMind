namespace MetaMind.Unity.Guis.Widgets.BlockViews.Options
{
    using Engine.Gui.Control.Item;
    using Engine.Gui.Control.Item.Layers;

    public class OptionItemLayer : BlockViewVerticalItemLayer
    {
        public OptionItemLayer(IViewItem item) 
            : base(item)
        {
        }

        public new OptionItemSettings ItemSettings
        {
            get { return (OptionItemSettings)base.ItemSettings; }
        }

        public new OptionItemLogic ItemLogic
        {
            get { return (OptionItemLogic)base.ItemLogic; }
        }

        public OptionItemFrameController ItemFrame
        {
            get { return this.ItemLogic.ItemFrame; }
        }
    }
}