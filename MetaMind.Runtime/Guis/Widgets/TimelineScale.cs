namespace MetaMind.Runtime.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class TimelineScale : ViewVisualComponent
    {
        protected TimelineScale(IView view, ViewSettings viewSettings, ItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        private float Scale { get; set; }
    }
}