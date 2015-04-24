namespace MetaMind.Runtime.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;

    public class TaskViewFactory : PointViewFactory2D
    {
        protected override dynamic CreateControl(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new TaskViewControl(view, (TaskViewSettings)viewSettings, (TaskItemSettings)itemSettings, new TaskItemFactory());
        }

        protected override IViewGraphics CreateGraphics(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new TaskViewGraphics(view, (TaskViewSettings)viewSettings, (TaskItemSettings)itemSettings);
        }
    }
}