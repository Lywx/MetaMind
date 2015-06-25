namespace MetaMind.Testimony.Guis.Widgets.Options
{
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Layers;

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