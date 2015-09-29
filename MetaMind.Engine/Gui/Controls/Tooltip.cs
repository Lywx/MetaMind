namespace MetaMind.Engine.Gui.Control
{
    using Controls;
    using Engine.Components.Graphics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class Tooltip : Control
    {
        #region Constructors 

        public Tooltip()
        {
        }

        #endregion

        public string Text { get; set; } = "";

        public override bool Visible
        {
            set
            {
                if (value && this.Text != null && this.Text != "" && Skin != null && Skin.Layers[0] != null)
                {
                    Vector2 size = Skin.Layers[0].Text.Font.Resource.MeasureString(this.Text);
                    this.Width = (int)size.X + Skin.Layers[0].ContentMargins.Horizontal;
                    this.Height = (int)size.Y + Skin.Layers[0].ContentMargins.Vertical;
                    Left = Mouse.GetState().X;
                    Top = Mouse.GetState().Y + 24;
                    base.Visible = value;
                }
                else
                {
                    base.Visible = false;
                }
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            CanFocus = false;
            Passive = true;
        }

        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            renderer.DrawLayer(this, Skin.Layers[0], rect);
            renderer.DrawString(this, Skin.Layers[0], this.Text, rect, true);
        }
    }
}
