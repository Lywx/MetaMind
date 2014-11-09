using System.Diagnostics;
using System.Windows.Forms;
using MetaMind.Engine.Guis.Widgets;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using MetaMind.Engine.Guis.Widgets.Views;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    public class MotivationItemTaskControl : ViewItemComponent
    {
        public MotivationItemTaskControl( IViewItem item )
            : base( item )
        {
        }

        public MotivationTracer Tracer { get; private set; }

        public void SelectIt()
        {
            if ( Tracer == null )
            {
                Tracer = new MotivationTracer( ItemControl );
            }
            Tracer.Show();
        }

        public bool UnselectIt()
        {
            if ( Tracer == null )
            {
                return true;
            }
            if ( Tracer.View.IsEnabled( ViewState.View_Has_Focus ) )
            {
                Tracer.Close();
            }
            return true;
        }

        public void UpdateStructure( GameTime gameTime )
        {
            if ( Tracer != null ) 
            {
                Tracer.UpdateStructure( gameTime );
            }
        }

        public void UpdateInput( GameTime gameTime )
        {
            if ( Tracer != null && Item.IsEnabled( ItemState.Item_Selected ) )
            {
                Tracer.UpdateInput( gameTime );
            }
        }
    }
}