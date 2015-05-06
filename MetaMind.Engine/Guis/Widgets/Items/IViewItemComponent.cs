namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items.Layers;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;

    public interface IViewItemComponent : IDisposable
    {
        #region View

        IView View { get; }

        T ViewGetLayer<T>() where T : class, IViewLayer;

        #endregion

        #region Item

        IViewItem Item { get; }

        T ItemGetLayer<T>() where T : class, IViewItemLayer;

        #endregion
    }
}