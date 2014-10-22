using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.WidgetGroups
{
    public interface IWidgetGroupControl
    {
        void HandleInput();

        void UpdateInput( GameTime gameTime );

        void UpdateStructure( GameTime gameTime );
    }

    public class WidgetGroupControl<TWidgetGroup, TWidgetGroupSettings, TWidgetGroupControl> : WidgetGroupComponent<TWidgetGroup, TWidgetGroupSettings, TWidgetGroupControl>, IWidgetGroupControl
        where TWidgetGroup : WidgetGroup<TWidgetGroupSettings>
        where TWidgetGroupControl : WidgetGroupControl<TWidgetGroup, TWidgetGroupSettings, TWidgetGroupControl>
    {
        public WidgetGroupControl( TWidgetGroup group )
            : base( group )
        {
        }

        public void HandleInput()
        {
        }

        public void UpdateInput( GameTime gameTime )
        {
        }

        public void UpdateStructure( GameTime gameTime )
        {
        }
    }
}