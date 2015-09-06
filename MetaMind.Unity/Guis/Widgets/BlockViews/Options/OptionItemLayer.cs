namespace MetaMind.Unity.Guis.Widgets.BlockViews.Options
{
    using Engine.Guis.Controls.Items;
    using Engine.Guis.Controls.Items.Layers;

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

        public OptionItemFrame ItemFrame
        {
            get { return this.ItemLogic.ItemFrame; }
        }
    }
}