namespace MetaMind.Engine.Guis.Widgets.Views
{
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public interface IPointViewControl
    {
        IViewItemFactory ItemFactory { get; }

        dynamic Scroll { get; }

        dynamic Selection { get; }

        IPointViewSwapControl Swap { get; }

        void SortItems(PointViewSortMode sortMode);

        void UpdateInput(GameTime gameTime);

        void UpdateStructure(GameTime gameTime);
    }
}