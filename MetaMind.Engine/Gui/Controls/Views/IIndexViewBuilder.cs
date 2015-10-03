namespace MetaMind.Engine.Gui.Controls.Views
{
    using Item;

    public interface IIndexViewBuilder
    {
        IMMViewNode Clone(IViewItem item);

        void Compose(IMMViewNode view, dynamic viewData);
    }
}