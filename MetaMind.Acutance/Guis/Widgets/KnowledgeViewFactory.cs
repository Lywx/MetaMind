namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;

    public class KnowledgeViewFactory : PointViewFactory2D
    {
        protected override dynamic CreateLogicControl(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new KnowledgeViewLogicControl(view, (KnowledgeViewSettings)viewSettings, (KnowledgeItemSettings)itemSettings, new KnowledgeItemFactory());
        }

        protected override IViewVisualControl CreateVisualControl(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new KnowledgeViewGraphics(view, (KnowledgeViewSettings)viewSettings, (KnowledgeItemSettings)itemSettings);
        }
    }
}