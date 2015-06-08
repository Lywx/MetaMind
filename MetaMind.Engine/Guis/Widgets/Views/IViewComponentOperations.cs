namespace MetaMind.Engine.Guis.Widgets.Views
{
    using Items;
    using Items.Layers;
    using Layers;

    public interface IViewComponentOperations
    {
        #region Layer

        T ViewGetLayer<T>() where T : class, IViewLayer;

        T ItemGetLayer<T>(IViewItem item) where T : class, IViewItemLayer;

        void SetupLayer();

        #endregion

        #region Components

        T ViewGetComponent<T>(string id) where T : class;

        #endregion
        
    }
}