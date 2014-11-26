namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
    using MetaMind.Perseverance.Guis.Widgets.Tasks.Views;

    public class TraceViewFactory : ViewBasicFactory2D
    {
        protected override dynamic CreateControl(IView view, ViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new TraceViewControl(view, (TaskViewSettings)viewSettings, (TaskItemSettings)itemSettings);
        }

        protected override IViewGraphics CreateGraphics(IView view, ViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new TaskViewGraphics(view, (TaskViewSettings)viewSettings, (TaskItemSettings)itemSettings);
        }
    }
}