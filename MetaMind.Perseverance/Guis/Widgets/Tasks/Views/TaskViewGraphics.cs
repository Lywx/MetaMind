using C3.Primtive2DXna;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Views
{
    public class TaskViewGraphics : ViewBasicGraphics
    {
        private int frameAlpha;

        public TaskViewGraphics( IView view, TaskViewSettings viewSettings, TaskItemSettings itemSettings )
            : base( view, viewSettings, itemSettings )
        {
        }

        public override void Draw( GameTime gameTime, byte alpha )
        {
            // draw active items
            base.Draw( gameTime, ( byte ) frameAlpha );

            DrawRegion( gameTime );
            DrawScrollBar( gameTime );
        }

        private void DrawScrollBar( GameTime gameTime )
        {
            ViewControl.ScrollBar.Draw( gameTime );
        }

        private void DrawRegion( GameTime gameTime )
        {
            if ( View.IsEnabled( ViewState.View_Has_Focus ) )
            {
                frameAlpha += 15;
                if ( frameAlpha > 255 )
                {
                    frameAlpha = 255;
                }
            }
            else
            {
                frameAlpha -= 15;
                if ( frameAlpha < 0 )
                {
                    frameAlpha = 0;
                }
            }
            Primitives2D.DrawRectangle( ScreenManager.SpriteBatch, RectangleExt.Extend( ViewControl.Region.Frame.Rectangle, ViewSettings.BorderMargin ), ColorExt.MakeTransparent( ViewSettings.CurrentColor, ( byte ) frameAlpha ), 2f );
            Primitives2D.FillRectangle( ScreenManager.SpriteBatch, ViewControl.Region.Frame.Rectangle, ColorExt.MakeTransparent( ViewSettings.CurrentColor, ( byte ) frameAlpha ) );
        }
    }
}