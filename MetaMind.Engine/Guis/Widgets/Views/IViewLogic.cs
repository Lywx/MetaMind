namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items;

    public interface IViewLogic : IViewScrollSupport, IViewSelectionSupport, IViewSwapSupport, IViewComponent, IInputable, IDisposable
    {
        IViewItemFactory ItemFactory { get; }
    }
}