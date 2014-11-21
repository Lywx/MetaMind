namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Banners
{
    using C3.Primtive2DXna;

    using MetaMind.Engine;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Elements.Views;

    using Microsoft.Xna.Framework;

    public class Banner : EngineObject
    {
        private ViewSettings1D viewSettings;
        private BannerSetting  bannerSetting;

        private TimelineText   past;
        private TimelineText   now;
        private TimelineText   future;
        
        private TimelineFlash  flash;

        public Banner(ViewSettings1D viewSettings, BannerSetting bannerSetting)
        {
            this.viewSettings  = viewSettings;
            this.bannerSetting = bannerSetting;

            this.past   = new TimelineText("Past"  , this.TextLeftmostPosition, 1f);
            this.now    = new TimelineText("Now"   , this.TextLeftmostPosition + new Vector2(270, 0), 1f);
            this.future = new TimelineText("Future", this.TextLeftmostPosition + new Vector2(270, 0) * 2, 1f);

            this.flash = new TimelineFlash(this.TextLeftmostPosition + new Vector2(0, 10));
        }

        private Vector2 TextLeftmostPosition
        {
            get 
            {
                return new Vector2(
                    viewSettings.StartPoint.X - 30,
                    viewSettings.StartPoint.Y - 63);
            }
        }

        public void Update(GameTime gameTime)
        {
            this.flash .Update(gameTime);
            this.past  .Update(gameTime);
            this.now   .Update(gameTime);
            this.future.Update(gameTime);
        }

        public void Draw(GameTime gameTime, byte alpha)
        {
            var spriteBatch  = ScreenManager.SpriteBatch;

            var upperBorder  = new Rectangle(0, viewSettings.StartPoint.Y - bannerSetting.Height / 2 - bannerSetting.Thin - bannerSetting.Thick, bannerSetting.Width, bannerSetting.Thick );
            var middleRegion = new Rectangle(0, viewSettings.StartPoint.Y - bannerSetting.Height / 2                                           , bannerSetting.Width, bannerSetting.Height);
            var lowerBorder  = new Rectangle(0, viewSettings.StartPoint.Y + bannerSetting.Height / 2 + bannerSetting.Thin                      , bannerSetting.Width, bannerSetting.Thick );

            Primitives2D.FillRectangle(spriteBatch, upperBorder , bannerSetting.Color.MakeTransparent(alpha));
            Primitives2D.FillRectangle(spriteBatch, lowerBorder , bannerSetting.Color.MakeTransparent(alpha));
            Primitives2D.FillRectangle(spriteBatch, middleRegion, bannerSetting.Color.MakeTransparent(alpha));

            this.flash .Draw(gameTime, alpha);
            this.past  .Draw(gameTime, alpha);
            this.now   .Draw(gameTime, alpha);
            this.future.Draw(gameTime, alpha);
        }
    }
}