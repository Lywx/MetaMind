using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Views
{
    public class TaskViewSettings : ViewSettings2D
    {
        //---------------------------------------------------------------------
        public Color                     CurrentColor      = new Color( 0, 20, 50, 200 );
        public Color                     ReceiverColor     = new Color( 255, 20, 0, 200 );
        //---------------------------------------------------------------------
        public byte                      AppearRate        = 24;
        public byte                      DisappearRate     = 48;
        //---------------------------------------------------------------------
        public TaskViewScrollBarSettings ScrollBarSettings = new TaskViewScrollBarSettings();

        public TaskViewSettings()
        {
            var itemSettings = new TaskItemSettings();
            
            ColumnNumDisplay = 1;
            ColumnNumMax     = 1;
            RowNumDisplay    = 10;
            RowNumMax        = 100;

            StartPoint = new Point( 200, 100 );
            RootMargin = new Point( itemSettings.NameFrameSize.X , itemSettings.NameFrameSize.Y + itemSettings.IdFrameSize.Y);
        }
    }
}