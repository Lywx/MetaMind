namespace MetaMind.Testimony.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items.Factories;
    using MetaMind.Engine.Guis.Widgets.Items.Settings;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.Factories;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Layouts;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.Scrolls;
    using MetaMind.Engine.Guis.Widgets.Views.Selections;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Swaps;
    using MetaMind.Engine.Guis.Widgets.Views.Visuals;

    public class PointView2DFactory : IViewFactory
    {
        public IViewLogic CreateLogic(IView view, ViewSettings viewSettings, ItemSettings itemSettings)
        {
            return new PointView2DLogic(view, new PointView2DScrollControl(view), new PointView2DSelectionControl(view), new PointViewHorizontalSwapControl(view), new PointView2DLayout(view), new ViewItemFactory());
        }

        public IViewVisual CreateVisual(IView view, ViewSettings viewSettings, ItemSettings itemSettings)
        {
            return new ViewVisual(view);
        }

        public IViewLayer CreateExtension(IView view)
        {
            return new PointView2DLayer(view);
        }
    }
}