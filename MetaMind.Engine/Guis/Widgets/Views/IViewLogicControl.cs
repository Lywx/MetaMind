namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items;

    public interface IViewLogicControl : IViewComponent, IInputable, IDisposable
    {
        IViewItemFactory ItemFactory { get; }

        dynamic Scroll { get; }

        dynamic Selection { get; }

        dynamic Swap { get; }
    }
}