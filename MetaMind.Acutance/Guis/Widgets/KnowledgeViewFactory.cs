namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.PointView;

    public class KnowledgeViewFactory : PointView2DFactory
    {
        protected override dynamic CreateLogicControl(IView view, PointView2DSettings viewSettings, ICloneable itemSettings)
        {
            return new KnowledgeViewLogic(view, (KnowledgeViewSettings)viewSettings, (KnowledgeItemSettings)itemSettings, new KnowledgeItemFactory());
        }

        protected override IViewVisualControl CreateVisualControl(IView view, PointView2DSettings viewSettings, ICloneable itemSettings)
        {
            return new KnowledgeViewGraphics(view, (KnowledgeViewSettings)viewSettings, (KnowledgeItemSettings)itemSettings);
        }
    }
}