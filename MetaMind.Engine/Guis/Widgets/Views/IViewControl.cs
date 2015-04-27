namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items;

    public interface IViewControl : IViewComponent, IInputable, IDisposable
    {
        IViewItemFactory ItemFactory { get; }

        dynamic Scroll { get; }

        dynamic Selection { get; }

        IPointViewSwapControl Swap { get; }

        void SortItems(PointViewSortMode sortMode);
    }
}