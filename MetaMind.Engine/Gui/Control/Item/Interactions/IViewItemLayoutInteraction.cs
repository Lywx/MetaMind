namespace MetaMind.Engine.Gui.Control.Item.Interactions
{
    using Layouts;

    public interface IViewItemLayoutInteraction
    {
        void ViewDoSelect(IViewItemLayout itemLayout);

        void ViewDoUnselect(IViewItemLayout itemLayout);

        bool ViewCanDisplay(IViewItemLayout itemLayout);
    }
}