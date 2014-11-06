using MetaMind.Engine.Guis.Widgets.Views;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Views
{
    public class TaskViewSettings : ViewSettings2D
    {
        public TaskViewScrollBarSettings ScrollBarSettings = new TaskViewScrollBarSettings();

        public TaskViewSettings()
        {
            ColumnNumDisplay = 3;
            StartPoint = new Point( 100, 100 );
            RootMargin = new Point( 100, 100 );
        }
    }
}