namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using Sprache;

    public interface IViewScrollSupport : IViewComponent, IInput, IDisposable
    {
        dynamic ViewScroll { get; }
    }
}