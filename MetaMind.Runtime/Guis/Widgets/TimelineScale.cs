namespace MetaMind.Runtime.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;

    public class TimelineScale : ViewVisualComponent
    {
        public TimelineScale(IView view, ICloneable viewSettings, ICloneable itemSettings)
            : base(view, viewSettings, itemSettings)
        {

        }

        private float Scale { get; set; }
    }
}