namespace MetaMind.Engine.Guis.Widgets.Views
{
    using Items;

    public interface IIndexViewCompositor
    {
        IView Clone(IViewItem item);

        void Compose(IView view, dynamic viewData);
    }
}