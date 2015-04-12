namespace MetaMind.Engine.Guis.Widgets.Views
{
    using MetaMind.Engine.Guis.Widgets.Items;

    public interface IPointViewControl : IInputable
    {
        IViewItemFactory ItemFactory { get; }

        dynamic Scroll { get; }

        dynamic Selection { get; }

        IPointViewSwapControl Swap { get; }

        void SortItems(PointViewSortMode sortMode);
    }
}