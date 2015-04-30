namespace MetaMind.Engine.Guis.Widgets.Items
{
    using MetaMind.Engine.Guis.Widgets.Views;

    public interface IViewItemComponent
    {
        #region Item Components

        IViewItem Item { get; }

        dynamic ItemLogic { get; }

        dynamic ItemData { get; }

        IItemVisual ItemVisual { get; }

        dynamic ItemSettings { get; }

        #endregion Item Components

        #region View Components

        IView View { get; }

        dynamic ViewLogic { get; }

        dynamic ViewSettings { get; }

        #endregion View Components
    }
}