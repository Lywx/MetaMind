using System;
using C3.Primtive2DXna;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Views
{

    public class TaskViewScrollBar : ViewComponent
    {
        private readonly TaskViewScrollBarSettings settings;
        private byte transitionAlpha;

        public TaskViewScrollBar( IView view, TaskViewSettings viewSettings, TaskItemSettings itemSettings, TaskViewScrollBarSettings scrollBarSettings )
            : base( view, viewSettings, itemSettings )
        {
            settings = scrollBarSettings;
            transitionAlpha = 0;
        }

        private Rectangle ScrollBarRectangle
        {
            get
            {
                var distance = ( float ) ViewControl.Scroll.YOffset / ( ViewControl.RowNum - ViewSettings.RowNumDisplay ) * ViewControl.Region.Height * ( 1 - ( float ) ViewSettings.RowNumDisplay / ViewControl.RowNum );
                // top boundary reaches the top of the ViewRegion
                // bottom boundary reaches the bottom of the ViewRegion
                return new Rectangle(
                    ViewControl.Region.X + ViewControl.Region.Width,
                    ViewControl.Region.Y + ( int ) Math.Ceiling( distance ),
                    settings.Width,
                    ViewControl.Region.Height * ViewSettings.RowNumDisplay / ViewControl.RowNum );
            }
        }

        public void Draw( GameTime gameTime )
        {
            if ( ViewControl.RowNum > ViewSettings.RowNumDisplay )
                Primitives2D.FillRectangle( ScreenManager.SpriteBatch, ScrollBarRectangle, settings.Color.MakeTransparent( transitionAlpha ) );
        }

        public void Trigger()
        {
            transitionAlpha = settings.BrightnessMax;
        }

        public void Upadte( GameTime gameTime )
        {
            if ( transitionAlpha > 2 )
                transitionAlpha -= settings.BrightnessTransitionRate;
            else
                transitionAlpha = 0;
        }
    }
}