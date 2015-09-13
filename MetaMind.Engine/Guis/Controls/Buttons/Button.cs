namespace MetaMind.Engine.Guis.Controls.Buttons
{
    using Components.Fonts;
    using Elements;
    using Microsoft.Xna.Framework;
    using Services;
    using Visuals;

    public class Button : Control
    {
        public Button(Rectangle rectangle)
        {
            this.Rectangle = rectangle;
        }

        public Button(Rectangle rectangle, ButtonSettings buttonSettings) : this(rectangle)
        {
            this.Label = new Label
            {
                TextPosition = () => this.Center.ToVector2(),
                TextHAlign   = StringHAlign.Center,
                TextVAlign   = StringVAlign.Center
            };

            this.Fill = new Box(
                () => this.Rectangle,
                () =>
                {
                    // Mouse is over when pressed
                    if (this[FrameState.Mouse_Is_Left_Pressed]() ||
                        this[FrameState.Mouse_Is_Right_Pressed]())
                    {
                        return buttonSettings.FillMousePressColor;
                    }
                    else if (this[FrameState.Mouse_Is_Over]())
                    {
                        return buttonSettings.FillMouseOverColor;
                    }

                    return buttonSettings.FillRegularColor;
                },
                () => true);

            this.Boundary = new Box(
                () => this.Rectangle,
                () =>
                {
                    // Mouse is over when pressed
                    if (this[FrameState.Mouse_Is_Left_Pressed]() ||
                        this[FrameState.Mouse_Is_Right_Pressed]())
                    {
                        return buttonSettings.BoundaryMousePressColor;
                    }
                    else if (this[FrameState.Mouse_Is_Over]())
                    {
                        return buttonSettings.BoundaryMouseOverColor;
                    }

                    return buttonSettings.BoundaryRegularColor;
                },
                () => false);
        }

        #region Dependency 

        public Box Boundary { get; set; }

        public Box Fill { get; set; }

        public Label Label { get; set; }

        public Glyph Glyph { get; set; }

        #endregion

        #region Draw

        public override void Draw(
            IGameGraphicsService graphics,
            GameTime time,
            byte alpha)
        {
            this.Boundary.Draw(graphics, time, alpha);
            this.Fill    .Draw(graphics, time, alpha);

            this.Label.Draw(graphics, time, alpha);

            base.Draw(graphics, time, alpha);
        }

        #endregion
    }
}