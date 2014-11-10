using System.Diagnostics;
using System.Threading.Tasks;
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

        public MotivationTaskTracer TaskTracer { get; private set; }

        public void SelectIt()
        {
            if ( TaskTracer == null )
            {
                TaskTracer = new MotivationTaskTracer( ItemControl, new MotivationTaskTracerSettings() );
                TaskTracer.Load();
            }
            TaskTracer.Show();
        }

        public bool UnselectIt()
        {
            if ( TaskTracer == null )
            {
                return true;
            }
            if ( TaskTracer.View.IsEnabled( ViewState.View_Has_Focus ) )
            {
                TaskTracer.Close();
            }
            return true;
        }

        public void UpdateStructure( GameTime gameTime )
        {
            if ( TaskTracer != null ) 
            {
                TaskTracer.UpdateStructure( gameTime );
            }
        }

        public void UpdateInput( GameTime gameTime )
        {
            if ( TaskTracer != null && Item.IsEnabled( ItemState.Item_Selected ) )
            {
                TaskTracer.UpdateInput( gameTime );
            }
        }
    }
}