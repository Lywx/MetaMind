using System;
using C3.Primtive2DXna;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Guis.Elements.Frames;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Items
{
    public class TaskItemGraphics : ViewItemBasicGraphics, IItemGraphics
    {
        #region Constructors

        public TaskItemGraphics( IViewItem item )
            : base( item )
        {
        }

        #endregion Constructors

        #region Structure Position

        protected Vector2 ExperienceCenter
        {
            get
            {
                return new Vector2(
                    ItemControl.ExperienceFrame.Center.X,
                    ItemControl.ExperienceFrame.Center.Y
                    );
            }
        }

        protected override Vector2 IdCenter
        {
            get
            {
                return new Vector2( ItemControl.IdFrame.Rectangle.Center.X, ItemControl.IdFrame.Rectangle.Center.Y );
            }
        }

        private Vector2 NameLocation
        {
            get
            {
                return new Vector2(
                    ItemControl.NameFrame.Location.X + ItemSettings.NameXLMargin,
                    ItemControl.NameFrame.Location.Y + ItemSettings.NameYTMargin
                    );
            }
        }
        private Rectangle RandomHighlight( GameTime gameTime, int flashLength, Rectangle rectangle )
        {
            var random = new Random();
            var thick = random.Next( flashLength );
            return new Rectangle(
                rectangle.X - thick,
                rectangle.Y - thick,
                rectangle.Width + thick * 2,
                rectangle.Height + thick * 2
                );
        }

        private Rectangle SinwaveHighlight( GameTime gameTime, int flashLength, Rectangle rectangle )
        {
            var thick = ( int ) ( flashLength * Math.Abs( Math.Sin( gameTime.TotalGameTime.TotalSeconds * 30 ) ) );
            return new Rectangle(
                rectangle.X - thick,
                rectangle.Y - thick,
                rectangle.Width + thick * 2,
                rectangle.Height + thick * 2
                );
        }

        #endregion Structure Position

        #region Update

        public void Update( GameTime gameTime )
        {
        }

        #endregion Update

        #region Draw


        public virtual void Draw( GameTime gameTime )
        {
            if ( !Item.IsEnabled( ItemState.Item_Active ) && !Item.IsEnabled( ItemState.Item_Dragging ) )
                return;

            DrawNameFrame();
            DrawName();
            DrawIdFrame();
            DrawId();
            DrawExperienceFrame();
            DrawProgressFrame();
            //DrawHourIndicator();
            //DrawHighlight( gameTime );
            //DrawSynchronization( gameTime );
        }

        protected virtual void DrawHighlight( GameTime gameTime )
        {
            //var questionData = ( ( QuestionData ) LivingData );
            //if ( questionData.IsHighlightedAsDowngrade ||
            //    questionData.IsHighlightedAsParent ||
            //    questionData.IsHighlightedAsChild
            //    )
            {
                Primitives2D.FillRectangle( ScreenManager.SpriteBatch, RandomHighlight( gameTime, 3, RectangleExt.Crop( ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameMargin ) ), ItemSettings.NameFrameHighlightColor );
            }
        }

        private void DrawExperienceFrame()
        {
            Primitives2D.FillRectangle( ScreenManager.SpriteBatch, RectangleExt.Crop( ItemControl.ExperienceFrame.Rectangle, ItemSettings.ExperienceFrameMargin ), ItemSettings.ExperienceFrameColor );
        }

        private void DrawIdFrame()
        {
            Primitives2D.FillRectangle( ScreenManager.SpriteBatch, RectangleExt.Crop( ItemControl.IdFrame.Rectangle, ItemSettings.IdFrameMargin ), ItemSettings.IdFrameColor );
        }

        private void DrawName()
        {
            //FontManager.DrawText( ItemSettings.NameFont, ItemData.Name, NameLocation, ItemSettings.NameColor, ItemSettings.NameSize );
            FontManager.DrawText( ItemSettings.NameFont, "TEMP Test samos jafso jsaof jo", NameLocation, ItemSettings.NameColor, ItemSettings.NameSize );
        }

        private void DrawNameFrame( Color color )
        {
            Primitives2D.DrawRectangle( ScreenManager.SpriteBatch, RectangleExt.Crop( ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameMargin ), color, 1f );
        }

        private void DrawNameFrame()
        {
            if ( Item.IsEnabled( ItemState.Item_Mouse_Over ) && Item.IsEnabled( ItemState.Item_Editing ) )
            {
                FillNameFrame( ItemSettings.NameFrameModificationColor );
                DrawNameFrame( ItemSettings.NameFrameMouseOverColor );
            }
            else if ( !ItemControl.NameFrame.IsEnabled( FrameState.Mouse_Over ) && Item.IsEnabled( ItemState.Item_Editing ) )
            {
                FillNameFrame( ItemSettings.NameFrameModificationColor );
            }
            else if ( ItemControl.NameFrame.IsEnabled( FrameState.Mouse_Over ) && Item.IsEnabled( ItemState.Item_Selected ) )
            {
                FillNameFrame( ItemSettings.NameFrameSelectionColor );
                DrawNameFrame( ItemSettings.NameFrameMouseOverColor );
            }
            else if ( ItemControl.NameFrame.IsEnabled( FrameState.Mouse_Over ) && !Item.IsEnabled( ItemState.Item_Selected ) )
            {
                DrawNameFrame( ItemSettings.NameFrameMouseOverColor );
            }
            else if ( Item.IsEnabled( ItemState.Item_Selected ) )
            {
                FillNameFrame( ItemSettings.NameFrameSelectionColor );
            }
            else
            {
                FillNameFrame( ItemSettings.NameFrameRegularColor );
            }
        }

        private void DrawProgressFrame()
        {
            Primitives2D.FillRectangle( ScreenManager.SpriteBatch, RectangleExt.Crop( ItemControl.ProgressFrame.Rectangle, ItemSettings.ProgressFrameMargin ), ItemSettings.ProgressFrameColor );
        }

        //protected virtual void DrawHourIndicator()
        //{
        //    var questionData = ( QuestionData ) LivingData;
        //    if ( !questionData.HasUpgrade )
        //        // no direction indicator
        //        FontManager.DrawCenteredText(
        //            ItemSettings.IdFont,
        //            "?",
        //            ExperienceCenter,
        //            ItemSettings.ErrorColor,
        //            ItemSettings.ExperienceSize
        //            );
        //    else
        //        // hour
        //        FontManager.DrawCenteredText(
        //            ItemSettings.IdFont,
        //            string.Format( "Hs: {0}", questionData.TotalExperience.Duration.TotalHours.ToString( "F0" ) ),
        //            ExperienceCenter,
        //            ItemSettings.ExperienceColor,
        //            ItemSettings.ExperienceSize
        //            );
        //}

        private void FillNameFrame( Color color )
        {
            Primitives2D.FillRectangle( ScreenManager.SpriteBatch, RectangleExt.Crop( ItemControl.NameFrame.Rectangle, ItemSettings.NameFrameMargin ), color );
        }

        //protected virtual void DrawSynchronization( GameTime gameTime )
        //{
        //    if ( LivingData.IsRunning )
        //    {
        //        Primitives2D.DrawRectangle( ScreenManager.SpriteBatch, SinwaveHighlight( gameTime, 10 ), ItemSettings.NameFrameSynchronizationColor, 2f );
        //    }
        //}

        #endregion Draw
    }
}