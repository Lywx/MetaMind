namespace MetaMind.Perseverance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Services;
    using MetaMind.Engine.Settings.Colors;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class MotivationItemSymbolGraphics : ViewItemComponent
    {
        private readonly Texture2D symbol;
        private          float     rotation;

        public MotivationItemSymbolGraphics(IViewItem item)
            : base(item)
        {
            var content = this.GameInterop.Content;
            this.symbol = content.Load<Texture2D>(@"Textures\UIs\Heart");
        }

        private Vector2 SymbolOrigin
        {
            get { return new Vector2(this.symbol.Width / 2f, this.symbol.Height / 2f); }
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!ItemControl.Active)
            {
                return;
            }

            if (Item.IsEnabled(ItemState.Item_Dragging))
            {
                this.DrawShadow(graphics);
            }

            this.DrawHeart(graphics, alpha);
        }

        public override void Update(GameTime time)
        {
            this.UpdateRotation();
        }

        private void DrawHeart(IGameGraphicsService graphics, byte alpha)
        {
            var flipped     = Math.Cos(this.rotation) > 0;
            var width       = ItemControl.SymbolFrame.Rectangle.Width;
            var height      = ItemControl.SymbolFrame.Rectangle.Height;
            var size        = new Point((int)(Math.Abs(Math.Cos(this.rotation)) * width), height);
            var destination = ExtRectangle.DestinationWithSize(ItemControl.SymbolFrame.Rectangle, size);

            graphics.SpriteBatch.Draw(
                this.symbol,
                destination,
                null,
                ItemSettings.SymbolFrameWishColor,
                0f,
                this.SymbolOrigin,
                flipped ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                0f);
        }

        private void DrawShadow(IGameGraphicsService graphics)
        {
            var scrollCenter = this.ViewControl.Scroll.RootCenterPoint(ItemControl.Id);
            var destination = new Rectangle(
                scrollCenter.X,
                scrollCenter.Y,
                ItemControl.RootFrame.Rectangle.Width,
                ItemControl.RootFrame.Rectangle.Height);

            graphics.SpriteBatch.Draw(
                this.symbol,
                destination,
                null,
                Palette.TransparentColor3,
                0f,
                this.SymbolOrigin,
                SpriteEffects.None,
                0f);
        }

        private void UpdateRotation()
        {
            if (Item.IsEnabled(ItemState.Item_Selected))
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