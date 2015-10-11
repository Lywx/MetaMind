namespace MetaMind.Engine.Entities.Controls.Item.Interactions
{
    using Microsoft.Xna.Framework;
    using Views;

    public interface IViewItemIndexViewProvider
    {
        IMMView IndexedView { get; }

        bool IndexedViewOpened { get; }

        void ViewUpdateIndexedView(GameTime time);

        void OpenIndexedView();

        void CloseIndexedView();
    }
}