namespace MetaMind.Acutance.Guis.Widgets
{
    using Microsoft.Xna.Framework;

    public class TraceViewSettings : TaskViewSettings
    {
        public TraceViewSettings(Point start)
            : base(start)
        {
        }

        public TraceViewSettings(Point start, Point margin)
            : base(start, margin)
        {
        }
    }
}