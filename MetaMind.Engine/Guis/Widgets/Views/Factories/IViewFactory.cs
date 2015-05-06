namespace MetaMind.Engine.Guis.Widgets.Views.Factories
{
    using MetaMind.Engine.Guis.Widgets.Items.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Visuals;

    public interface IViewFactory
    {
        IViewLogic CreateLogic(IView view, ViewSettings viewSettings, ItemSettings itemSettings);

        IViewVisual CreateVisual(IView view, ViewSettings viewSettings, ItemSettings itemSettings);

        IViewLayer CreateExtension(IView view);
    }
}