namespace MetaMind.Engine.Guis.Widgets.Views
{
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public interface IViewControl
    {
        IViewItemFactory ItemFactory { get; }

        dynamic Scroll { get; }

        dynamic Selection { get; }

        IViewSwapControl Swap { get; }

        void SortItems(ViewSortMode sortMode);

        void UpdateInput(GameTime gameTime);

        void UpdateStructure(GameTime gameTime);
    }
}