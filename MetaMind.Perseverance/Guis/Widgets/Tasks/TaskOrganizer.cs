using MetaMind.Engine.Guis.Widgets;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Views;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks
{
    public class TaskOrganizer : Widget // may use module or group
    {
        private IView questionView;
        private IView directionView;
        private IView futureView;

        public TaskOrganizer()
        {
            var viewFactory = new TaskViewFactory();
            questionView = new View( new TaskViewSettings(), new TaskItemSettings(), viewFactory );
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            questionView.Draw( gameTime, alpha);
            //directionView.Draw( gameTime );
            //futureView   .Draw( gameTime );
        }

        public override void UpdateInput( GameTime gameTime )
        {
            questionView .UpdateInput( gameTime );
            //directionView.UpdateInput( gameTime );
            //futureView   .UpdateInput( gameTime );
        }

        public override void UpdateStructure( GameTime gameTime )
        {
            questionView .UpdateStructure( gameTime );
            //directionView.UpdateStructure( gameTime );
            //futureView   .UpdateStructure( gameTime );
        }
    }
}