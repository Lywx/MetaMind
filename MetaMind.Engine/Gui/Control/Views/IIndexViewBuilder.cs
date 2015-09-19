namespace MetaMind.Engine.Gui.Control.Views
{
    using Item;

    public interface IIndexViewBuilder
    {
        IView Clone(IViewItem item);

        void Compose(IView view, dynamic viewData);
    }
}