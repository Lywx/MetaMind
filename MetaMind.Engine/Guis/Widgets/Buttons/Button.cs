namespace MetaMind.Engine.Guis.Widgets.Buttons
{
    using System;
    using Elements;
    using Microsoft.Xna.Framework;
    using Services;
    using Visuals;

    public class Button : GameControllableEntity
    {
        public Button(Rectangle buttonRectangle, ButtonSettings buttonSettings)
        {
            this.Frame = new PickableFrame(buttonRectangle);

            this.BoxFilled = new Box(
                () => this.Frame.Rectangle,
                () =>
                {
                    if (this.Frame[FrameState.Mouse_Is_Over]())
                    {
                        return buttonSettings.MouseOverColor;
                    }

                    if (this.Frame[FrameState.Mouse_Is_Left_Pressed]() &&
                        this.Frame[FrameState.Mouse_Is_Right_Pressed]())
                    {
                        return buttonSettings.MousePressedColor;
                    }

                    return buttonSettings.RegularColor;
                },
                () => true);

            this.BoxBoundary = new Box(
                () => this.Frame.Rectangle,
                () =>
                {
                    if (this.Frame[FrameState.Mouse_Is_Over]())
                    {
                        return buttonSettings.BoundaryMouseOverColor;
                    }

                    if (this.Frame[FrameState.Mouse_Is_Left_Pressed]() &&
                        this.Frame[FrameState.Mouse_Is_Right_Pressed]())
                    {
                        return buttonSettings.BoundaryMousePressedColor;
                    }

                    return buttonSettings.BoundaryRegularColor;
                },
                () => false);

            this.OnMouseLeftPressed = () => {};
            this.OnMouseLeftDoubleClicked = () => {};
            this.OnMouseRightPressed = () => {};
            this.OnMouseRightDoubleClicked = () => {};

            this.Frame.MouseLeftPressed += (sender, args) => this.OnMouseLeftPressed();
            this.Frame.MouseLeftDoubleClicked += (sender, args) => this.OnMouseLeftDoubleClicked();
            this.Frame.MouseRightPressed += (sender, args) => this.OnMouseRightPressed();
            this.Frame.MouseRightDoubleClicked += (sender, args) => this.OnMouseRightDoubleClicked();
        }

        private Box BoxFilled { get; set; }

        private Box BoxBoundary { get; set; }

        private PickableFrame Frame { get; set; }

        public Action OnMouseLeftPressed { get; set; }

        public Action OnMouseLeftDoubleClicked { get; set; }

        public Action OnMouseRightPressed { get; set; }

        public Action OnMouseRightDoubleClicked { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.BoxBoundary.Draw(graphics, time, alpha);
            this.BoxFilled  .Draw(graphics, time, alpha);
            base            .Draw(graphics, time, alpha);
        }

        public override void Update(GameTime time)
        {
            this.Frame.Update(time);
            base.Update(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.Frame.UpdateInput(input, time);
            base.UpdateInput(input, time);
        }
    }
}