namespace MetaMind.Engine.Guis.Widgets.Items.Layouts
{
    using MetaMind.Engine.Guis.Widgets.Views;

    public interface IViewItemSorter : IViewComponent
    {
    }

    public class ViewItemSorter : ViewComponent, IViewItemSorter
    {
        public ViewItemSorter(IView view)
            : base(view)
        {
        }
    }
}