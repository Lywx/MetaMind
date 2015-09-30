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
                () => this.Rectangle.Bounds,
                () => Color.White);

            this.Label = new Label
            {
                AnchorLocation = () => this.Rectangle.Center.ToVector2(),

                TextHAlignment = HoritonalAlignment.Center,
                TextVAlignment = VerticalAlignment.Center
            };
        }

        private void InitializeInput(Rectangle rectangle)
        {
            this.Rectangle = new DraggableRectangle(rectangle);
            this.Children.Add(this.Rectangle);
        }

        #endregion

        #region Dependency 

        /// <summary>
        /// The rectangle handles related input.
        /// </summary>
        public DraggableRectangle Rectangle { get; set; }

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

        public override void Draw(
            IMMEngineGraphicsService graphics,
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