namespace MetaMind.Engine.Entities.Controls.Views
{
    using Item;

    public interface IIndexViewBuilder
    {
        IMMView Clone(IMMViewItem item);

        void Compose(IMMView view, dynamic viewData);
    }
}