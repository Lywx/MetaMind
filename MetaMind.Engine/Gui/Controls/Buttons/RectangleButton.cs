namespace MetaMind.Engine.Gui.Controls.Buttons
{
    using System;
    using Elements;
    using Elements.Rectangles;
    using Engine.Components.Graphics.Fonts;
    using Images;
    using Labels;
    using Microsoft.Xna.Framework;
    using Service;

    public class RectangleButton : Control
    {
        #region Constructors

        public RectangleButton(ControlManager manager, Rectangle rectangle)
            : this(manager, new RectangleButtonSettings(), rectangle)
        {
        }

        public RectangleButton(ControlManager manager, RectangleButtonSettings settings, Rectangle rectangle) : base(manager)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            this.InitializeInput(rectangle);
            this.InitializeVisual(manager, settings);
        }

        #endregion

        #region Initialization

        private void InitializeVisual(ControlManager manager, RectangleButtonSettings settings)
        {
            this.Image = new ImageBox(
                manager,
                () =>
                {
                    if (this.Rectangle[ElementState.Mouse_Is_Left_Pressed]() ||
                        this.Rectangle[ElementState.Mouse_Is_Right_Pressed]())
                    {
                        return settings.Image.MousePressed;
                    }
                    else if (this.Rectangle[ElementState.Mouse_Is_Left_Released]() ||
                             this.Rectangle[ElementState.Mouse_Is_Left_Released]())
                    {
                        return settings.Image.MouseReleased;
                    }
                    else if (this.Rectangle[ElementState.Mouse_Is_Over]())
                    {
                        return settings.Image.MouseOver;
                    }

                    return settings.Image.MouseOut;
                },
                () => this.Rectangle.Bounds,
                () => Color.White);

            this.Label = new Label(manager)
            {
                AnchorLocation = () => this.Rectangle.Center.ToVector2(),
                TextHAlignment = HoritonalAlignment.Center,
                TextVAlignment = VerticalAlignment.Center
            };
        }

        private void InitializeInput(Rectangle rectangle)
        {
            this.Rectangle = new DraggableRectangle(rectangle);
        }

        #endregion

        #region Dependency 

        /// <summary>
        /// The rectangle handles related input>
        /// </summary>
        public DraggableRectangle Rectangle { get; set; }

        /// <summary>
        /// Button image.
        /// </summary>
        public ImageBox Image { get; set; }

        public Label Label { get; set; }

        #endregion

        #region Draw

        public override void Draw(
            IGameGraphicsService graphics,
            GameTime time,
            byte alpha)
        {
            base.Draw(graphics, time, alpha);

            this.Image.Draw(graphics, time, alpha);
            this.Label.Draw(graphics, time, alpha);
        }

        #endregion
    }
}