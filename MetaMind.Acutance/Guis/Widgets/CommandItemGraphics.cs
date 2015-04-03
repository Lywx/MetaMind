namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Components.Fonts;
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

            this.FillNameFrameWith(ItemSettings.NameFrameRunningColor, alpha);

            this.DrawName(255);
            this.DrawExperienceFrame(255);
            this.DrawExperience(255);
            this.DrawIdFrame(255);
            this.DrawId(255);
        }

        protected override void DrawName(byte alpha)
        {
            string name = ItemData.Name;

            string text = FontManager.CropMonoSpacedText(
                ItemControl.Id > 0 ? Format.Paddle(name, View.Items[ItemControl.Id - 1].ItemData.Name) : name,
                ItemSettings.NameSize,
                ItemSettings.NameFrameSize.X - ItemSettings.NameXLMargin * 2);

            FontManager.DrawMonoSpacedText(
                ItemSettings.NameFont,
                text,
                this.NameLocation,
                ColorExt.MakeTransparent(ItemSettings.NameColor, alpha),
                ItemSettings.NameSize);
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