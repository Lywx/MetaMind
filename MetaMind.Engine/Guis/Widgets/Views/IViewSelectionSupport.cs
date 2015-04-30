namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using Sprache;

    public interface IViewSelectionSupport : IViewComponent, IInput, IDisposable
    {
        dynamic ViewSelection { get; }
    }
}