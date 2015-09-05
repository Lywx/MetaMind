namespace MetaMind.Engine.Guis.Controls.Views
{
    using Items;

    public interface IIndexViewCompositor
    {
        IView Clone(IViewItem item);

        void Compose(IView view, dynamic viewData);
    }
}