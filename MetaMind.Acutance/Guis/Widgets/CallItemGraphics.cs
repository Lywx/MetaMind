namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Concepts;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items;

    public class CallItemGraphics : TraceItemGraphics
    {
        public CallItemGraphics(IViewItem item)
            : base(item)
        {
        }

        protected override void DrawExperience(byte alpha)
        {
            var countdown = (TimeSpan)ItemData.Timeout - ((Experience)ItemData.Experience).Duration;
            FontManager.DrawCenteredText(
                ItemSettings.ExperienceFont,
                string.Format("{0:hh\\:mm\\:ss}", countdown),
                this.ExperienceLocation,
                ColorExt.MakeTransparent(ItemSettings.ExperienceColor, alpha),
                ItemSettings.ExperienceSize);
        }

        protected override void DrawNameFrame(byte alpha)
        {
            base.DrawNameFrame(alpha);

            switch ((CallEntry.EventState)ItemData.State)
            {
                case CallEntry.EventState.Running:
                    {
                        this.FillNameFrameWith(ItemSettings.NameFrameRunningColor, alpha);
                    }

                    break;

                case CallEntry.EventState.Transiting:
                    {
                        this.FillNameFrameWith(ItemSettings.NameFrameTransitionColor, alpha);
                    }

                    break;
            }
        }
    }
}