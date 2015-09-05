namespace MetaMind.Engine.Guis.Controls.Items.Interactions
{
    using Microsoft.Xna.Framework;
    using Views;

    public interface IViewItemIndexViewProvider
    {
        IView IndexedView { get; }

        bool IndexedViewOpened { get; }

        void ViewUpdateIndexedView(GameTime time);

        void OpenIndexedView();

        void CloseIndexedView();
    }
}