namespace MetaMind.Engine.Guis.Widgets.Items
{
    using Layers;
    using Views.Layers;

    public interface IViewItemComponentOperations
    {
        #region Layer

        void SetupLayer();

        T ItemGetLayer<T>() where T : class, IViewItemLayer;

        T ViewGetLayer<T>() where T : class, IViewLayer;

        #endregion
    }
}