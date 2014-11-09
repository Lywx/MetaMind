using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Engine.Settings;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Views
{
    public class TaskViewSettings : ViewSettings2D
    {
        //---------------------------------------------------------------------
        public Color                     CurrentColor      = ColorPalette.TransparentColor1;
        public Color                     ReceiverColor     = new Color( 255, 20, 0, 200 );
        //---------------------------------------------------------------------
        public byte                      AppearRate        = 24;
        public byte                      DisappearRate     = 48;
        //---------------------------------------------------------------------
        public Point                     BorderMargin      = new Point( 4, 4 );
        //---------------------------------------------------------------------
        public TaskViewScrollBarSettings ScrollBarSettings = new TaskViewScrollBarSettings();

        public TaskViewSettings()
        {
            var itemSettings = new TaskItemSettings();
            
            RootMargin = new Point( itemSettings.NameFrameSize.X , itemSettings.NameFrameSize.Y + itemSettings.IdFrameSize.Y);
        }
    }
}