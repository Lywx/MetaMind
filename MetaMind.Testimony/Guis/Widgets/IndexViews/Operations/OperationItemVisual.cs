namespace MetaMind.Testimony.Guis.Widgets.IndexViews.Operations
{
    using Engine.Guis.Widgets.Items;
    using Tests;

    public class OperationItemVisual : TestItemVisual
    {
        public OperationItemVisual(IViewItem item) : base(item)
        {
        }

        public override void SetupLayer()
        {
            base.SetupLayer();

            this.DescriptionLabel.Label.Text = () => "ASD";
            //this.DescriptionLabel.Label.T
        }
    }
}
