using System;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Views
{
    public class TaskViewFactory : ViewBasicFactory2D
    {
        public override dynamic CreateControl( IView view, ICloneable viewSettings, ICloneable itemSettings )
        {
            return new TaskViewControl( view, ( TaskViewSettings ) viewSettings, ( TaskItemSettings ) itemSettings );
        }

        public override IViewGraphics CreateGraphics( IView view, ICloneable viewSettings, ICloneable itemSettings )
        {
            return new TaskViewGraphics( view, ( TaskViewSettings ) viewSettings, ( TaskItemSettings ) itemSettings );
        }
    }
}