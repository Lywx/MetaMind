namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Concepts;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class CommandItemGraphics : TraceItemGraphics
    {
        public CommandItemGraphics(IViewItem item)
            : base(item)
        {
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            if (!ItemControl.Active && !Item.IsEnabled(ItemState.Item_Dragging))
            {
                return;
            }

            this.DrawNameFrame(255);

            switch ((CommandState)ItemData.State)
            {
                case CommandState.Running:
                    {
                        this.FillNameFrameWith(ItemSettings.NameFrameRunningColor, alpha);
                    }

                    break;

                case CommandState.Transiting:
                    {
                        this.FillNameFrameWith(ItemSettings.NameFrameTransitionColor, alpha);
                    }

                    break;
            }

            this.DrawName(255);
            this.DrawExperienceFrame(255);
            this.DrawExperience(255);
            this.DrawIdFrame(255);
            this.DrawId(255);
        }

        protected override void DrawExperience(byte alpha)
        {
            var countdown = ((Experience)ItemData.Experience).Duration;
            FontManager.DrawCenteredText(
                ItemSettings.ExperienceFont,
                string.Format("{0:hh\\:mm\\:ss}", countdown),
                this.ExperienceLocation,
                ColorExt.MakeTransparent(ItemSettings.ExperienceColor, alpha),
                ItemSettings.ExperienceSize);
        }
    }
}