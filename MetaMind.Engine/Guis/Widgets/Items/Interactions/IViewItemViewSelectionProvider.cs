namespace MetaMind.Engine.Guis.Widgets.Items.Interactions
{
    public interface IViewItemViewSelectionProvider
    {
        void ViewDoSelect();

        void ViewDoUnselect();

        void ViewUpdateSelection();
    }
}