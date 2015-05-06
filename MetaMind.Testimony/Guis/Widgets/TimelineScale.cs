namespace MetaMind.Runtime.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Views;

    public class TimelineScale : ViewVisualComponent
    {
        protected TimelineScale(IView view)
            : base(view)
        {
        }

        private float Scale { get; set; }
    }
}