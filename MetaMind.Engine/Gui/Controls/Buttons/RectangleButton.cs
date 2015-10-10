namespace MetaMind.Engine.Gui.Controls.Buttons
{
    using System;
    using Elements.Rectangles;
    using Graphics.Fonts;
    using Images;
    using Labels;
    using Microsoft.Xna.Framework;
    using Services;

    public class RectangleButton : MMControlComponent
    {
        #region Constructors

        public RectangleButton(MMControlManager manager, Rectangle rectangle)
            : this(manager, new RectangleButtonSettings(), rectangle)
        {
        }

        public RectangleButton(MMControlManager manager, RectangleButtonSettings settings, Rectangle rectangle) : base(manager)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            this.Settings = settings;

            this.InitializeInput(rectangle);
            this.InitializeVisual();
        }

        #endregion

        #region Dependency

        public RectangleButtonSettings Settings { get; set; }

        #endregion

        #region Initialization

        private void InitializeVisual()
        {
            this.Image = new ImageBox(
                ,
                () => this.ImmRectangle.Bounds,
                () => Color.White);

            this.Label = new Label
            {
                AnchorLocation = () => this.ImmRectangle.Center.ToVector2(),

                TextHAlignment = HoritonalAlignment.Center,
                TextVAlignment = VerticalAlignment.Center
            };
        }

        private void InitializeInput(Rectangle rectangle)
        {
            this.ImmRectangle = new MMDraggableRectangleElement(rectangle);
            this.Children.Add(this.ImmRectangle);
        }

        #endregion

        #region Dependency 

        /// <summary>
        /// The rectangle handles related input.
        /// </summary>
        public MMDraggableRectangleElement ImmRectangle { get; set; }

        /// <summary>
        /// Button image.
        /// </summary>
        public ImageBox Image { get; set; }

        public Label Label { get; set; }

        #endregion

        public override void Update(GameTime time)
        {
            base.Update(time);
        }

        #region Draw

        public override void Draw(GameTime time)
        {
            base.Draw(time);

            this.Image.Draw(time);
            this.Label.Draw(time);
        }

        #endregion
    }
}