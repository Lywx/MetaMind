namespace MetaMind.Engine.Gui.Controls.Item.Interactions
{
    using Microsoft.Xna.Framework;
    using Views;

    public interface IViewItemIndexViewProvider
    {
        IMMViewNode IndexedView { get; }

        bool IndexedViewOpened { get; }

        void ViewUpdateIndexedView(GameTime time);

        void OpenIndexedView();

        void CloseIndexedView();
    }
}