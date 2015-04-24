namespace MetaMind.Runtime.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class TaskViewSettings : PointGridSettings
    {
        public TaskViewSettings(Point start)
            : base(start)
        {
        }

        public TaskViewSettings(Point start, Point margin)
            : base(start, margin)
        {
        }
    }
}