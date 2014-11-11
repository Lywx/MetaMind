using System;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Views
{
    public class TaskViewFactory : ViewBasicFactory2D
    {
        protected override dynamic CreateControl( IView view, ViewSettings2D viewSettings, ICloneable itemSettings )
        {
            return new TaskViewControl( view, ( TaskViewSettings ) viewSettings, ( TaskItemSettings ) itemSettings );
        }

        protected override IViewGraphics CreateGraphics( IView view, ViewSettings2D viewSettings, ICloneable itemSettings )
        {
            return new TaskViewGraphics( view, ( TaskViewSettings ) viewSettings, ( TaskItemSettings ) itemSettings );
        }
    }
}