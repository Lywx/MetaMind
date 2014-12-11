namespace MetaMind.Engine.Guis.Widgets.Views
{
    using Microsoft.Xna.Framework;

    // TODO: Encapsulation is broken
    public interface IViewControl
    {
        IViewSwapControl Swap { get; }

        dynamic Scroll { get; }

        dynamic Selection { get; }

        void SortItems(ViewSortMode sortMode);

        void UpdateInput(GameTime gameTime);

        void UpdateStructure(GameTime gameTime);
    }
}