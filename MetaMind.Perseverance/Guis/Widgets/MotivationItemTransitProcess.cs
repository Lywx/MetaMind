namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class MotivationItemTransitProcess : ViewItemTransitProcess
    {
        public MotivationItemTransitProcess(IViewItem srcItem, IView desView)
            : base(srcItem, desView)
        {
        }

        protected override void Transit()
        {
            // remove data in original view
            this.SrcItem.ViewControl.ItemFactory.RemoveData(this.SrcItem);

            // change data position
            //MotivationSpace desMotivationList = this.DesView.ViewSettings.Space;
            //    desMotivationList.Insert(position, this);

            var position = this.DesSelection.PreviousSelectedId != null
                               ? (int)this.DesSelection.PreviousSelectedId
                               : 0;

            //this.SrcItem.ItemData.CopyToSpace(desMotivationList, position);

            base.Transit();
        }
    }
}