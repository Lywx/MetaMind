namespace MetaMind.Engine.Guis.Controls.Items.Interactions
{
    using Microsoft.Xna.Framework;

    public interface IViewItemViewSelectionProvider
    {
        void ViewDoSelect();

        void ViewDoUnselect();

        void ViewUpdateSelection(GameTime time);
    }
}