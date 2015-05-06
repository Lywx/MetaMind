namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.Factories;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.PointView;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Visuals;

    public class KnowledgeViewFactory : PointView2DFactory
    {
        protected override IViewLogic CreateLogic(IView view, PointView2DSettings viewSettings, ICloneable itemSettings)
        {
            return new KnowledgeViewLogic(view, (KnowledgeViewSettings)viewSettings, (KnowledgeItemSettings)itemSettings, new KnowledgeItemFactory());
        }

        protected override IViewVisual CreateVisual(IView view, PointView2DSettings viewSettings, ICloneable itemSettings)
        {
            return new KnowledgeViewGraphics(view, (KnowledgeViewSettings)viewSettings, (KnowledgeItemSettings)itemSettings);
        }
    }
}