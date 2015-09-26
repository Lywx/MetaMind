namespace MetaMind.Engine.Gui.Controls.Item.Interactions
{
    using Microsoft.Xna.Framework;

    public interface IViewItemViewSelectionProvider
    {
        void ViewDoSelect();

        void ViewDoUnselect();

        void ViewUpdateSelection(GameTime time);
    }
}