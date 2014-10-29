namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemViewControl2D : ViewItemViewControl1D
    {
        public ViewItemViewControl2D( IViewItem item )
            : base( item )
        {
        }

        protected override void UpdateViewScroll()
        {
            base.UpdateViewScroll();

            ItemControl.Row = ItemControl.View.Control.RowFrom( ItemControl.Id );
            ItemControl.Column = ItemControl.View.Control.ColumnFrom( ItemControl.Id );
        }
    }
}