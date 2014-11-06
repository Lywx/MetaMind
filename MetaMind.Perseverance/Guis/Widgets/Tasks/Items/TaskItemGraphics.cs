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

        public TaskItemGraphics(IViewItem item)
            : base(item)
        {
        }

        #endregion

        #region Structure Position

        private Rectangle HighLightRandomFrame(GameTime gameTime, int flashLength)
        {
            var baseRectanle = ItemControl.NameFrame.Rectangle.Crop();
            var random = new Random();
            var thick = random.Next(flashLength);
            return new Rectangle(
                baseRectanle.X - thick,
                baseRectanle.Y - thick,
                baseRectanle.Width + thick*2,
                baseRectanle.Height + thick*2
                );
        }

        private Rectangle HighLightSinFrame(GameTime gameTime, int flashLength)
        {
            var baseRectanle = ItemControl.NameFrame.Rectangle.Crop();
            var thick = (int) (flashLength*Math.Abs(Math.Sin(gameTime.TotalGameTime.TotalSeconds*30)));
            return new Rectangle(
                baseRectanle.X - thick,
                baseRectanle.Y - thick,
                baseRectanle.Width + thick*2,
                baseRectanle.Height + thick*2
                );
        }

        protected virtual Vector2 IdPositionC
        {
            get
            {
                return new Vector2(
                    TileIdRectangle.Center.X,
                    TileIdRectangle.Center.Y
                    );
            }
        }

        protected virtual Rectangle TileIdRectangle
        {
            get
            {
                return new Rectangle(
                    Center.X,
                    Center.Y + ItemSettings.IdFrameMargin.Y - ItemSettings.IdFrameSize.Y,
                    ItemSettings.IdFrameSize.X - ItemSettings.IdFrameMargin.X,
                    ItemSettings.IdFrameSize.Y - ItemSettings.IdFrameMargin.Y*2);
            }
        }

        protected Vector2 NamePositionLT
        {
            get
            {
                return new Vector2(
                    Center.X + ItemSettings.NameXLMargin,
                    Center.Y + ItemSettings.NameYTMargin
                    );
            }
        }


        protected Vector2 ExperiencePositionC
        {
            get
            {
                return new Vector2(
                    ExperienceRectangle.Center.X,
                    ExperienceRectangle.Center.Y);
            }
        }

        protected Rectangle ExperienceRectangle
        {
            get
            {
                return new Rectangle(
                    Center.X + ItemSettings.IdFrameSize.X,
                    Center.Y + ItemSettings.ExperienceFrameMargin.Y - ItemSettings.ExperienceFrameSize.Y,
                    ItemSettings.ExperienceFrameSize.X - ItemSettings.ExperienceFrameMargin.X*2,
                    ItemSettings.ExperienceFrameSize.Y - ItemSettings.ExperienceFrameMargin.Y*2
                    );
            }
        }


        #endregion

        public virtual void Draw( GameTime gameTime )
        {
            if ( !Item.IsEnabled( ItemState.Item_Active ) && !Item.IsEnabled( ItemState.Item_Dragging ) )
                return;

            // name frame
            DrawNameFrame();
            // name
            //FontManager.DrawText( ItemSettings.NameFont, ViewItemLabelType.Name.GetLabelFrom( ItemData ), NamePositionLT( Frame.Rectangle ), ItemSettings.NameColor, ItemSettings.NameSize );
            // id frame
            //Primitives2D.FillRectangle( ScreenManager.SpriteBatch, TileIdRectangle( Frame.Rectangle ), ItemSettings.IdFrameColor );
            //// id and folder indicator
            //DrawIdIndicator();
            //// hours frame
            //Primitives2D.FillRectangle( ScreenManager.SpriteBatch, ExperienceRectangle, ItemSettings.ExperienceFrameColor );
            //// hours and direction indicator
            //DrawHourIndicator();
            //// highlight
            //DrawHighlight( gameTime );
            //// sync
            //DrawSynchronization( gameTime );
        }

        public void Update( GameTime gameTime )
        {
        }

        //protected virtual void DrawHighlight( GameTime gameTime )
        //{
        //    var questionData = ( ( QuestionData ) LivingData );
        //    if ( questionData.IsHighlightedAsDowngrade
        //         || questionData.IsHighlightedAsParent
        //         || questionData.IsHighlightedAsChild
        //        )
        //    {
        //        Primitives2D.FillRectangle( ScreenManager.SpriteBatch, HighLightRandomFrame( gameTime, 3 ), ItemSettings.NameFrameHighlightColor );
        //    }
        //}

        //protected virtual void DrawHourIndicator()
        //{
        //    var questionData = ( QuestionData ) LivingData;
        //    if ( !questionData.HasUpgrade )
        //        // no direction indicator
        //        FontManager.DrawCenteredText(
        //            ItemSettings.IdFont,
        //            "?",
        //            ExperiencePositionC,
        //            ItemSettings.ErrorColor,
        //            ItemSettings.ExperienceSize
        //            );
        //    else
        //        // hour
        //        FontManager.DrawCenteredText(
        //            ItemSettings.IdFont,
        //            string.Format( "Hs: {0}", questionData.TotalExperience.Duration.TotalHours.ToString( "F0" ) ),
        //            ExperiencePositionC,
        //            ItemSettings.ExperienceColor,
        //            ItemSettings.ExperienceSize
        //            );
        //}

        //protected void DrawIdIndicator()
        //{
        //    if ( !LivingData.Folder.Enabled )
        //        // folder indicator
        //        FontManager.DrawCenteredText(
        //            ItemSettings.IdFont,
        //            "x",
        //            IdPositionC( Frame.Rectangle ),
        //            ItemSettings.ErrorColor,
        //            ItemSettings.IdSize
        //            );
        //    else
        //        // id
        //        FontManager.DrawCenteredText( ItemSettings.IdFont, ItemControl.Id.ToString( new CultureInfo( "en-US" ) ), IdPositionC, ItemSettings.IdColor, ItemSettings.IdSize );
        //}

        private void FillNameFrame( Color color )
        {
            Primitives2D.FillRectangle( ScreenManager.SpriteBatch, ItemControl.NameFrame.Rectangle.Crop( ItemSettings.NameFrameMargin ), color );
        }

        private void DrawNameFrame( Color color )
        {
            Primitives2D.FillRectangle( ScreenManager.SpriteBatch, ItemControl.NameFrame.Rectangle.Crop( ItemSettings.NameFrameMargin ), color, 1f );
        }

        private void DrawNameFrame()
        {
            Primitives2D.DrawCircle( ScreenManager.SpriteBatch, ( ( Rectangle ) ItemControl.RootFrame.Rectangle ).Center.ToVector2(), 5f, 20, Color.White );
            
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

        //protected virtual void DrawSynchronization( GameTime gameTime )
        //{
        //    if ( LivingData.IsRunning )
        //    {
        //        Primitives2D.DrawRectangle( ScreenManager.SpriteBatch, HighLightSinFrame( gameTime, 10 ), ItemSettings.NameFrameSynchronizationColor, 2f );
        //    }
        //}

    }
}