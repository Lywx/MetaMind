namespace MetaMind.Engine.Guis.Widgets.Items
{
    using MetaMind.Engine.Guis.Widgets.Views;

    public interface IViewItemComponent
    {
        #region Item Components

        IViewItem Item { get; }

        dynamic ItemControl { get; }

        dynamic ItemData { get; }

        IItemVisualControl ItemVisualControl { get; }


        // TODO: I felt like i don;t need to have a itemsettings in view
        dynamic ItemSettings { get; }

        #endregion Item Components

        #region View Components

        IView View { get; }

        dynamic ViewControl { get; }

        dynamic ViewSettings { get; }

        #endregion View Components
    }
}