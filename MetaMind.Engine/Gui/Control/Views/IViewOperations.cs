namespace MetaMind.Engine.Gui.Control.Views
{
    using Layers;

    public interface IViewOperations
    {
        #region Layer

        T GetLayer<T>() where T : class, IViewLayer;

        #endregion
    }
}