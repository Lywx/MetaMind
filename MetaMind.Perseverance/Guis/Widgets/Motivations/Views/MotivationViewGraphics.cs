using C3.Primtive2DXna;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Views
{
    public class MotivationViewGraphics : ViewBasicGraphics
    {
        private int frameAlpha;

        public MotivationViewGraphics(IView view, MotivationViewSettings viewSettings, MotivationItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            base.Draw(gameTime, alpha);

            DrawRegion(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (View.IsEnabled(ViewState.View_Has_Focus))
            {
                frameAlpha += 15;
                if (frameAlpha > 255)
                {
                    frameAlpha = 255;
                }
            }
            else
            {
                frameAlpha -= 15;
                if (frameAlpha < 0)
                {
                    frameAlpha = 0;
                }
            }
        }

        private void DrawRegion(GameTime gameTime)
        {
            Primitives2D.FillRectangle(ScreenManager.SpriteBatch, ViewControl.Region.Frame.Rectangle, ColorExt.MakeTransparent(ViewSettings.HighlightColor, (byte)frameAlpha));
        }
    }
}