namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using Microsoft.Xna.Framework;

    public interface IViewComponent : IUpdateable, IDisposable 
    {
        IView View { get; }
    }
}