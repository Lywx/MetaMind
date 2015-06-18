namespace MetaMind.Engine.Guis.Widgets.Views
{
    using Items;

    public interface IIndexViewComposer
    {
        void Compose(IView view, dynamic viewData);

        IView Construct(IViewItem item);
    }
}