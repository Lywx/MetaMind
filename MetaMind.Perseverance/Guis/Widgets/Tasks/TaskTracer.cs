using MetaMind.Engine.Guis.Widgets;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Views;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks
{
    using MetaMind.Engine.Guis.Elements.Views;

    public class TaskTracer : Widget
    {
        private IView            view;
        private TaskViewFactory  viewFactory  = new TaskViewFactory();
        private TaskViewSettings viewSettings = new TaskViewSettings { StartPoint = new Point( 15, 15 ) };
        private TaskItemSettings itemSettings = new TaskItemSettings();

        public TaskTracer()
        {
            view = new View( viewSettings, itemSettings, viewFactory );
        }

        public override void Draw( GameTime gameTime, byte alpha )
        {
            view.Draw( gameTime, alpha );
        }

        public override void UpdateInput( GameTime gameTime )
        {
            view.UpdateInput( gameTime );
        }

        public override void UpdateStructure( GameTime gameTime )
        {
            view.UpdateStructure( gameTime );
        }
    }
}