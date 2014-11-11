using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IViewControl
    {
        IViewSwapControl Swap { get; }
        dynamic Scroll { get; }

        dynamic Selection { get; }

        void SortItems( ViewSortMode sortMode );

        void UpdateInput( GameTime gameTime );

        void UpdateStrucutre( GameTime gameTime );
    }
}