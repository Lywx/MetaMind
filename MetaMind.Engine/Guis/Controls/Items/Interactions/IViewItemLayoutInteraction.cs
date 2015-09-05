namespace MetaMind.Engine.Guis.Widgets.Items.Interactions
{
    using MetaMind.Engine.Guis.Widgets.Items.Layouts;

    public interface IViewItemLayoutInteraction
    {
        void ViewDoSelect(IViewItemLayout itemLayout);

        void ViewDoUnselect(IViewItemLayout itemLayout);

        bool ViewCanDisplay(IViewItemLayout itemLayout);
    }
}