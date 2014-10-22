using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.WidgetGroups
{
    public interface IWidgetGroupGraphics
    {
        void Draw( GameTime gameTime );

        void Update( GameTime gameTime );
    }

    public class WidgetGroupGraphics<TWidgetGroup, TWidgetGroupSettings, TWidgetGroupControl> : WidgetGroupComponent<TWidgetGroup, TWidgetGroupSettings, TWidgetGroupControl>, IWidgetGroupGraphics
        where TWidgetGroup : WidgetGroup<TWidgetGroupSettings>
        where TWidgetGroupControl : WidgetGroupControl<TWidgetGroup, TWidgetGroupSettings, TWidgetGroupControl>
    {
        public WidgetGroupGraphics( TWidgetGroup group )
            : base( group )
        {
        }

        public void Draw( GameTime gameTime )
        {
        }

        public void Update( GameTime gameTime )
        {
        }
    }
}