using System;
using C3.Primtive2DXna;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Guis.Elements.Regions;
using MetaMind.Engine.Guis.Widgets.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Views
{
    public class TaskViewRegion : Region, IViewComponent
    {
        private int frameAlpha;

        public TaskViewRegion( IView view, ICloneable viewSettings, ICloneable itemSettings )
            : base( StartRectangle( viewSettings, itemSettings ) )
        {
            View         = view;
            ViewSettings = viewSettings;
            ItemSettings = itemSettings;
        }

        public dynamic ItemSettings { get; private set; }

        public IView View { get; private set; }

        public dynamic ViewControl { get { return View.Control; } }

        public dynamic ViewSettings { get; private set; }

        public void Draw( GameTime gameTime )
        {
            if ( View.IsEnabled( ViewState.View_Has_Focus ) )
            {
                frameAlpha += 15;
                if ( frameAlpha > 255 )
                    frameAlpha = 255;

                ScreenManager.SpriteBatch.End();
                ScreenManager.SpriteBatch.Begin( SpriteSortMode.BackToFront, BlendState.Additive );

                Primitives2D.DrawRectangle( ScreenManager.SpriteBatch, Frame.Rectangle, ColorExt.MakeTransparent( ViewSettings.CurrentColor, ( byte ) frameAlpha ), 2f );

                ScreenManager.SpriteBatch.End();
                ScreenManager.SpriteBatch.Begin();
            }
            else
            {
                frameAlpha = 0;
            }
        }

        private static Rectangle StartRectangle( dynamic viewSettings, dynamic itemSettings )
        {
            return new Rectangle(
                viewSettings.StartPoint.X,
                viewSettings.StartPoint.Y,
                viewSettings.ColumnNumDisplay * itemSettings.NameFrameSize.X,
                viewSettings.RowNumDisplay * ( itemSettings.NameFrameSize.Y + itemSettings.IdFrameSize.Y )
                );
        }
    }
}