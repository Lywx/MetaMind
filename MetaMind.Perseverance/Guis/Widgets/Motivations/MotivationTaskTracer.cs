using System.Reflection;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Guis.Modules;
using MetaMind.Engine.Guis.Widgets;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations
{
    public class MotivationTaskTracer : Module<MotivationTaskTracerSettings>
    {
        private readonly MotivationItemControl hostControl;
        
        private readonly IView                 view;

        public MotivationTaskTracer( MotivationItemControl itemControl, MotivationTaskTracerSettings settings )
            : base( settings )
        {
            hostControl = itemControl;
            view        = new View( Settings.ViewSettings, Settings.ItemSettings, Settings.ViewFactory );
        }

        public override void Load()
        {
            LoadData();
        }

        private void LoadData() 
        {
            foreach ( var task in hostControl.ItemData.Tasks )
            {
                view.Control.AddItem( task );
            }
        }

        public IView View
        {
            get { return view; }
        }

        public void Close()
        {
            // clear highlight
            View.Control.Selection.Clear();
            View.Disable( ViewState.View_Active );
        }

        public override void Draw( GameTime gameTime, byte alpha )
        {
            View.Draw( gameTime, alpha );
        }

        public void Show()
        {
            View.Enable( ViewState.View_Active );
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
            View.ViewSettings.StartPoint = Vector2Ext.ToPoint( hostControl.RootFrame.Center.ToVector2() + hostControl.ViewSettings.TracerMargin );

            View.UpdateStructure( gameTime );
        }
    }
}