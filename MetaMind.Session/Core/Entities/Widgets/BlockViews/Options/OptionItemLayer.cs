namespace MetaMind.Session.Guis.Widgets.BlockViews.Options
{
    using Engine.Core.Entity.Control.Item;
    using Engine.Core.Entity.Control.Item.Layers;

    public class OptionItemLayer : MMBlockViewVerticalItemLayer
    {
        public OptionItemLayer(IMMViewItem item) 
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