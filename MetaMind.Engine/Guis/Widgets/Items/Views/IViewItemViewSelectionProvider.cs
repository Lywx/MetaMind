namespace MetaMind.Engine.Guis.Widgets.Items.Views
{
    public interface IViewItemViewSelectionProvider
    {
        void ViewDoSelect();

        void ViewDoUnselect();

        void ViewSelectionUpdate();
    }
}