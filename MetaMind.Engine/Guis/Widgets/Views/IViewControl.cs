namespace MetaMind.Engine.Guis.Widgets.Views
{
    using Microsoft.Xna.Framework;

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