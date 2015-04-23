namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class Banner 
    {
        private PointViewSettings1D viewSettings;
        private BannerSetting  bannerSetting;

        private TimelineText   past;
        private TimelineText   now;
        private TimelineText   future;

        private TimelineFlash  flash;


        public Banner(PointViewSettings1D viewSettings, BannerSetting bannerSetting)
        {
            this.viewSettings  = viewSettings;
            this.bannerSetting = bannerSetting;

            this.past   = new TimelineText("Past"  , this.TextLeftmostPosition,                           1f, Font.UiRegular);
            this.now    = new TimelineText("Now"   , this.TextLeftmostPosition + new Vector2(270, 0),     1f, Font.UiRegular);
            this.future = new TimelineText("Future", this.TextLeftmostPosition + new Vector2(270, 0) * 2, 1f, Font.UiRegular);
                
            this.flash = new TimelineFlash(this.TextLeftmostPosition + new Vector2(0, 10));
        }

        private Vector2 TextLeftmostPosition
        {
            get 
            {
                return new Vector2(
                    this.viewSettings.PointStart.X - 30,
                    this.viewSettings.PointStart.Y - 63);
            }
        }

        public void Update(GameTime gameTime)
        {
            this.flash.Update(gameTime);
        }

        public void Draw(GameTime gameTime, byte alpha)
        {
            var spriteBatch  = ScreenManager.SpriteBatch;

            var upperBorder  = new Rectangle(0, this.viewSettings.PointStart.Y - this.bannerSetting.Height / 2 - this.bannerSetting.Thin - this.bannerSetting.Thick, this.bannerSetting.Width, this.bannerSetting.Thick );
            var middleRegion = new Rectangle(0, this.viewSettings.PointStart.Y - this.bannerSetting.Height / 2                                           , this.bannerSetting.Width, this.bannerSetting.Height);
            var lowerBorder  = new Rectangle(0, this.viewSettings.PointStart.Y + this.bannerSetting.Height / 2 + this.bannerSetting.Thin                      , this.bannerSetting.Width, this.bannerSetting.Thick );

            Primitives2D.FillRectangle(spriteBatch, upperBorder , this.bannerSetting.Color.MakeTransparent(alpha));
            Primitives2D.FillRectangle(spriteBatch, lowerBorder , this.bannerSetting.Color.MakeTransparent(alpha));
            Primitives2D.FillRectangle(spriteBatch, middleRegion, this.bannerSetting.Color.MakeTransparent(alpha));

            this.flash .Draw(gameTime, alpha);
            this.past  .Draw(gameTime, alpha);
            this.now   .Draw(gameTime, alpha);

            this.future.Draw(gameTime, alpha);
        }
    }
}