using MetaMind.Engine.Extensions;
using MetaMind.Engine.Guis.Widgets;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Items;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Views;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations
{
    public class MotivationTracer : Widget 
    {
        private readonly MotivationItemControl itemControl;
        
        private readonly IView                 view;
        private readonly Vector2               viewMargin;

        private TaskItemSettings itemSettings = new TaskItemSettings();
        private TaskViewFactory  viewFactory  = new TaskViewFactory();
        private TaskViewSettings viewSettings = new TaskViewSettings
        {
            ColumnNumDisplay = 1, 
            ColumnNumMax     = 1, 
            RowNumDisplay    = 9, 
            RowNumMax        = 100,
        };


        public MotivationTracer( MotivationItemControl itemControl)
        {
            this.itemControl = itemControl;

            view       = new View( viewSettings, itemSettings, viewFactory );
            viewMargin = new Vector2( -itemSettings.NameFrameSize.X / 2f, 80 );

            foreach ( var task in itemControl.ItemData.Tasks )
            {
                View.Control.AddItem( task );
            }
        }

        public IView View
        {
            get { return view; }
        }

        public void Close()
        {
            View.Control.Selection.Clear();
        }

        public override void Draw( GameTime gameTime, byte alpha )
        {
            View.Draw( gameTime, alpha );
        }

        public void Show()
        {
            // show up tracer
            View.Control.Selection.Select( 0 );
        }
        public override void UpdateInput( GameTime gameTime )
        {
            View.UpdateInput( gameTime );
        }

        public override void UpdateStructure( GameTime gameTime )
        {
            // make sure that task region and task items all follow the host location changes
            View.ViewSettings.StartPoint = Vector2Ext.ToPoint( itemControl.RootFrame.Center.ToVector2() + viewMargin );
            View.Control.Region.Location = Vector2Ext.ToPoint( itemControl.RootFrame.Center.ToVector2() + viewMargin );

            View.UpdateStructure( gameTime );
        }
    }
}