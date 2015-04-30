namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using Sprache;

    public interface IViewSwapSupport : IViewComponent, IInput, IDisposable
    {
        dynamic ViewSwap { get; }
    }
}