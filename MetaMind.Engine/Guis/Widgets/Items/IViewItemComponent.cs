namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    using Layers;
    using Views;
    using Views.Layers;

    public interface IViewItemComponent : IGameControllableEntity, IDisposable
    {
        IView View { get; }

        IViewItem Item { get; }

        #region Layering

        void SetupLayer();

        T ItemGetLayer<T>() where T : class, IViewItemLayer;

        T ViewGetLayer<T>() where T : class, IViewLayer;

        #endregion
    }
}