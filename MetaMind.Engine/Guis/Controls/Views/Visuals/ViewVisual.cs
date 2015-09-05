namespace MetaMind.Engine.Guis.Controls.Views.Visuals
{
    using Items;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Services;
    using IDrawable = Engine.IDrawable;

    public class ViewVisual : ViewVisualComponent, IViewVisual
    {
        protected RenderTarget2D renderTarget;

        public ViewVisual(IView view)
            : base(view)
        {
            this.renderTarget = new RenderTarget2D(
                this.GraphicsDevice,
                900,
                900,
                false,
                this.GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);
        }

        protected GraphicsDevice GraphicsDevice => this.Graphics.Manager.GraphicsDevice;

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.DrawBegin(graphics, this.renderTarget);

            this.DrawComponents(graphics, time, alpha);
            this.DrawItems(graphics, time, alpha);

            this.DrawEnd(graphics);

            graphics.SpriteBatch.Draw(this.renderTarget, new Rectangle(0, 0, 900, 900), Color.White);
        }

        protected virtual void DrawComponents(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            foreach (var component in this.ViewComponents)
            {
                ((IDrawable)component.Value).Draw(graphics, time, alpha);
            }
        }

        protected virtual void DrawItems(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            foreach (var item in this.View.ItemsRead.ToArray())
            {
                // TODO: Possible separation of active and inactive storage and looping to improve cpu performace
                // TODO: Possible separate implementation for different views (1d or 2d views)
                if (item[ItemState.Item_Is_Active]())
                {
                    item.Draw(graphics, time, alpha);
                }
            }
        }
        /// <summary>
        /// Draws the entire scene in the given render target.
        /// </summary>
        /// <returns>A texture2D with the scene drawn in it.</returns>
        protected void DrawBegin(IGameGraphicsService graphics, RenderTarget2D renderTarget)
        {
            graphics.SpriteBatch.End();

            // Set the render target
            this.GraphicsDevice.SetRenderTarget(renderTarget);

            this.GraphicsDevice.DepthStencilState = new DepthStencilState { DepthBufferEnable = true };

            //GraphicsDevice.Clear(new Color(0, 0, 0, 0));

            graphics.SpriteBatch.Begin();
        }

        protected void DrawEnd(IGameGraphicsService graphics)
        {
            graphics.SpriteBatch.End();

            // Drop the render target
            this.GraphicsDevice.SetRenderTarget(null);

            graphics.SpriteBatch.Begin();
        }
    }
}