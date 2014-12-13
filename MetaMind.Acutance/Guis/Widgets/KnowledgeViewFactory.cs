namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;

    public class KnowledgeViewFactory : ViewBasicFactory2D
    {
        protected override dynamic CreateControl(IView view, ViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new KnowledgeViewControl(view, (KnowledgeViewSettings)viewSettings, (KnowledgeItemSettings)itemSettings, new KnowledgeItemFactory());
        }

        protected override IViewGraphics CreateGraphics(IView view, ViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new KnowledgeViewGraphics(view, (KnowledgeViewSettings)viewSettings, (KnowledgeItemSettings)itemSettings);
        }
    }
}