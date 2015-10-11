namespace MetaMind.Engine.Entities.Controls.Item.Interactions
{
    using Layouts;

    public interface IViewItemLayoutInteraction
    {
        void ViewDoSelect(IMMViewItemLayout itemLayout);

        void ViewDoUnselect(IMMViewItemLayout itemLayout);

        bool ViewCanDisplay(IMMViewItemLayout itemLayout);
    }
}