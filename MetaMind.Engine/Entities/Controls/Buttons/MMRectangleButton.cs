namespace MetaMind.Engine.Entities.Controls.Buttons
{
    using Elements.Rectangles;
    using Graphics.Fonts;
    using Images;
    using Labels;
    using Microsoft.Xna.Framework;
    using Nodes;
    using System;

    public class MMButton : MMNode
    {
        public MMButton()
        {
            this.Setup();
        }

        private void Setup()
        {
            this.Add(this.Image);
        }
    }

    public class MMButtonRenderer : MMNodeRenderer
    {
        public MMButtonRenderer(IMMNode target) : base(target)
        {
        }
    }

    public class MMRectangleButton : MMControlComponent
    {
        #region Constructors

        public MMRectangleButton(MMControlManager manager, Rectangle rectangle)
            : this(manager, new MMRectangleButtonSettings(), rectangle)
        {
        }

        public MMRectangleButton(
            MMControlManager manager,
            MMRectangleButtonSettings settings,
            Rectangle rectangle) : base(manager)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            this.Settings = settings;

            this.SetupInput(rectangle);
            this.SetupRenderer();
        }

        #endregion

        #region Dependency

        public MMRectangleButtonSettings Settings { get; set; }

        #endregion

        #region Initialization

        private void SetupRenderer()
        {
            // TODO::::
            //this.Image = new ImageBox(
            //    ,
            //    () => this.Rectangle.Bounds,
            //    () => Color.White);

            this.Label = new Label
            {
                AnchorLocation = () => this.Rectangle.Center.ToVector2(),

                TextHAlignment = HoritonalAlignment.Center,
                TextVAlignment = VerticalAlignment.Center
            };
        }

        private void SetupInput(Rectangle rectangle)
        {
            this.Rectangle = new MMDraggableRectangleElement(rectangle);
            this.Children.Add(this.Rectangle);
        }

        #endregion

        #region Dependency

        /// <summary>
        /// The rectangle handles related input.
        /// </summary>
        public MMDraggableRectangleElement Rectangle { get; set; }

        /// <summary>
        /// Button image.
        /// </summary>
        public MMImageBox Image { get; set; }

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