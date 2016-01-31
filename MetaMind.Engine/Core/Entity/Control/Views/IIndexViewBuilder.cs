namespace MetaMind.Engine.Core.Entity.Control.Views
{
    using Item;

    public interface IIndexViewBuilder
    {
        IMMView Clone(IMMViewItem item);

        void Compose(IMMView view, dynamic viewData);
    }
}