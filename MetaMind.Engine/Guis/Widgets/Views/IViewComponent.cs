namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;
    using Items;
    using Items.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;

    using Microsoft.Xna.Framework;

    public interface IViewComponent : IGameControllableEntity, IUpdateable, IDisposable 
    {
        IView View { get; }

        #region Layering

        T ViewGetLayer<T>() where T : class, IViewLayer;

        T ItemGetLayer<T>(IViewItem item) where T : class, IViewItemLayer;

        void SetupLayer();

        #endregion

        #region Components

        T ViewGetComponent<T>(string id) where T : class;

        #endregion
    }
}