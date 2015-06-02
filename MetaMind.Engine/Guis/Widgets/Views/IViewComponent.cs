namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views.Layers;

    using Microsoft.Xna.Framework;

    public interface IViewComponent : IUpdateable, IDisposable 
    {
        IView View { get; }

        #region Layering

        T ViewGetLayer<T>() where T : class, IViewLayer;

        T ViewGetComponent<T>(string id) where T : class;

        void SetupLayer();

        #endregion
    }
}