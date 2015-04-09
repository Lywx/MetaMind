namespace MetaMind.Perseverance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;

    public class TimelineScale : ViewVisualComponent
    {
        private float scale;

        public TimelineScale(IView view, ICloneable viewSettings, ICloneable itemSettings)
            : base(view, viewSettings, itemSettings)
        {

        }
    }
}