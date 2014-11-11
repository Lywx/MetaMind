using C3.Primtive2DXna;
using MetaMind.Engine;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Guis.Widgets.Views;

using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Banners
{
    public class Banner : EngineObject
    {
        private ViewSettings1D    viewSettings;
        private ViewBannerSetting bannerSetting;

        private TimelineText past;
        private TimelineText now;
        private TimelineText future;
        
        private TimelineFlash flash;

        public Banner(ViewSettings1D viewSettings, ViewBannerSetting bannerSetting)
        {
            this.viewSettings  = viewSettings;
            this.bannerSetting = bannerSetting;

            this.past   = new TimelineText("Past"  , this.TextLeftmostPosition);
            this.now    = new TimelineText("Now"   , this.TextLeftmostPosition + new Vector2(270, 0));
            this.future = new TimelineText("Future", this.TextLeftmostPosition + new Vector2(270, 0) * 2);

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
            flash .Update(gameTime);
            past  .Update(gameTime);
            now   .Update(gameTime);
            future.Update(gameTime);
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

            flash .Draw(gameTime);
            past  .Draw(gameTime);
            now   .Draw(gameTime);
            future.Draw(gameTime);
        }
    }
}