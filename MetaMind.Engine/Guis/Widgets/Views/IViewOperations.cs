namespace MetaMind.Engine.Guis.Widgets.Views
{
    using Layers;

    public interface IViewOperations
    {
        #region Layer

        T GetLayer<T>() where T : class, IViewLayer;

        #endregion
    }
}