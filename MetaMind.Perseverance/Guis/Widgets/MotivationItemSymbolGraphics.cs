namespace MetaMind.Perseverance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class MotivationItemSymbolGraphics : ViewItemComponent
    {
        private readonly Texture2D symbolTexture;
        private          float     rotation;

        public MotivationItemSymbolGraphics(IViewItem item)
            : base(item)
        {
            this.symbolTexture = ContentManager.Load<Texture2D>(@"Textures\UIs\Heart");
        }

        private Vector2 SymbolOrigin
        {
            get { return new Vector2(this.symbolTexture.Width / 2f, this.symbolTexture.Height / 2f); }
        }

        public void Draw(GameTime gameTime, byte alpha)
        {
            if (!this.ItemControl.Active)
            {
                return;
            }

            if (this.Item.IsEnabled(ItemState.Item_Dragging))
            {
                this.DrawShadow();
            }

            this.DrawHeart(alpha);
        }

        public void Update(GameTime gameTime)
        {
            this.UpdateRotation();
        }

        private void DrawHeart(byte alpha)
        {
            var flipped     = Math.Cos(this.rotation) > 0;
            var width       = this.ItemControl.SymbolFrame.Rectangle.Width + this.ItemData.Attraction;
            var height      = this.ItemControl.SymbolFrame.Rectangle.Height + this.ItemData.Attraction;
            var size        = new Point((int)(Math.Abs(Math.Cos(this.rotation)) * width), height);
            var destination = RectangleExt.DestinationWithSize(this.ItemControl.SymbolFrame.Rectangle, size);

            if (this.ItemData.Property == "Neutral")
            {
                ScreenManager.SpriteBatch.Draw(
                    this.symbolTexture,
                    destination,
                    null,
                    this.Item.IsEnabled(ItemState.Item_Selected)
                        ? ColorPalette.TransparentColor5
                        : ColorPalette.TransparentColor3,
                    0f,
                    this.SymbolOrigin,
                    flipped ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                    0f);
            }
            else if (this.ItemData.Property == "Wish")
            {
                ScreenManager.SpriteBatch.Draw(
                    this.symbolTexture,
                    destination,
                    null,
                    this.ItemSettings.WishColor,
                    0f,
                    this.SymbolOrigin,
                    flipped ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                    0f);
            }
            else if (this.ItemData.Property == "Fear")
            {
                ScreenManager.SpriteBatch.Draw(
                    this.symbolTexture,
                    destination,
                    null,
                    this.ItemSettings.FearColor,
                    0f,
                    this.SymbolOrigin,
                    flipped ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                    0f);
            }
        }

        private void DrawShadow()
        {
            var scrollCenter = this.ViewControl.Scroll.RootCenterPoint(this.ItemControl.Id);
            var destination = new Rectangle(
                scrollCenter.X,
                scrollCenter.Y,
                this.ItemControl.RootFrame.Rectangle.Width,
                this.ItemControl.RootFrame.Rectangle.Height);

            ScreenManager.SpriteBatch.Draw(
                this.symbolTexture,
                destination,
                null,
                ColorPalette.TransparentColor3,
                0f,
                this.SymbolOrigin,
                SpriteEffects.None,
                0f);
        }

        private void UpdateRotation()
        {
            if (this.Item.IsEnabled(ItemState.Item_Selected))
            {
                this.rotation += MathHelper.ToRadians(5);
            }
            else
            {
                if (this.rotation > MathHelper.ToRadians(360))
                {
                    this.rotation -= MathHelper.ToRadians(365);
                }
                else if (this.rotation > MathHelper.ToRadians(0))
                {
                    this.rotation -= MathHelper.ToRadians(5);
                }
            }
        }
    }
}