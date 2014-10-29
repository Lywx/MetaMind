using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Views
{
    public class TaskViewGraphics : ViewBasicGraphics
    {
        public TaskViewGraphics( IView view, TaskViewSettings viewSettings, TaskItemSettings itemSettings )
            : base( view, viewSettings, itemSettings )
        {
        }

        public override void Draw( GameTime gameTime )
        {
            base.Draw( gameTime );

            ViewControl.Region.Draw( gameTime );
            ViewControl.ScrollBar.Draw( gameTime );
        }
    }
}