namespace MetaMind.Engine.Gui.Control.Button
{
    using Component.Graphics.Font;
    using Element;
    using Element.Rectangles;
    using Microsoft.Xna.Framework;
    using Service;
    using Visuals;

    public class RectangleButton : Control
    {
        // TODO(Button): How to initialize from constructor?
        public RectangleButton(Rectangle rectangle)
            : this(rectangle, new ButtonSettings())
        {
        }

        public RectangleButton(Rectangle rectangle, ButtonSettings buttonSettings) 
        {
            this.Rectangle = new DraggableRectangle(rectangle);

            // Default Behavior
            this.Label = new Label
            {
                AnchorLocation = () => this.Rectangle.Center.ToVector2(),
                TextHAlignment = HoritonalAlignment.Center,
                TextVAlignment = VerticalAlignment.Center
            };

            this.Fill = new Box(
                () => this.Rectangle,
                () =>
                {
                    // Mouse is over when pressed
                    if (this.Rectangle[ElementState.Mouse_Is_Left_Pressed]() ||
                        this[ElementState.Mouse_Is_Right_Pressed]())
                    {
                        return buttonSettings.FillMousePressColor;
                    }
                    else if (this[ElementState.Mouse_Is_Over]())
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
                    if (this[ElementState.Mouse_Is_Left_Pressed]() ||
                        this[ElementState.Mouse_Is_Right_Pressed]())
                    {
                        return buttonSettings.BoundaryMousePressColor;
                    }
                    else if (this[ElementState.Mouse_Is_Over]())
                    {
                        return buttonSettings.BoundaryMouseOverColor;
                    }

                    return buttonSettings.BoundaryRegularColor;
                },
                () => false);
        }

        #region Dependency 

        public DraggableRectangle Rectangle { get; set; }

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
            base.Draw(graphics, time, alpha);

            this.Boundary.Draw(graphics, time, alpha);
            this.Fill    .Draw(graphics, time, alpha);

            this.Label.Draw(graphics, time, alpha);
        }

        #endregion
    }
}