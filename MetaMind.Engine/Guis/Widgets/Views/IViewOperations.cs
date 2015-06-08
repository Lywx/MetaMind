namespace MetaMind.Engine.Guis.Widgets.Views
{
    using Layers;

    public interface IViewOperations
    {
        #region Layer

        T GetLayer<T>() where T : class, IViewLayer;

        void SetupLayer();

        #endregion

        #region Binding

        void SetupBinding();

        #endregion
    }
}