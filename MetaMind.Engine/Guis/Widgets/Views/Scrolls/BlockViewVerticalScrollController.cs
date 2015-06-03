namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    public class BlockViewVerticalScrollController : PointViewVerticalScrollController
    {
        public BlockViewVerticalScrollController(IView view) : base(view)
        {
        }

        public override void Zoom(int row)
        {
            base.Zoom(row);
        }
    }
}