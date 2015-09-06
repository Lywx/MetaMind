namespace MetaMind.Engine.Guis.Controls.Buttons
{
    using System;
    using Components.Fonts;
    using Elements;
    using Microsoft.Xna.Framework;
    using Services;
    using Visuals;

    public class Button : GameControllableEntity
    {
        public Button(Rectangle buttonRectangle, ButtonSettings buttonSettings)
        {
            this.Frame = new PickableFrame(buttonRectangle);
            this.Label = new Label
            {
                TextPosition = () => this.Frame.Center.ToVector2(),
                TextHAlign   = StringHAlign.Center,
                TextVAlign   = StringVAlign.Center
            };

            this.BoxFilled = new Box(
                () => this.Frame.Rectangle,
                () =>
                {
                    // Mouse is over when pressed
                    if (this.Frame[FrameState.Mouse_Is_Left_Pressed]() ||
                        this.Frame[FrameState.Mouse_Is_Right_Pressed]())
                    {
                        return buttonSettings.MousePressedColor;
                    }
                    else if (this.Frame[FrameState.Mouse_Is_Over]())
                    {
                        return buttonSettings.MouseOverColor;
                    }

                    return buttonSettings.RegularColor;
                },
                () => true);

            this.BoxBoundary = new Box(
                () => this.Frame.Rectangle,
                () =>
                {
                    // Mouse is over when pressed
                    if (this.Frame[FrameState.Mouse_Is_Left_Pressed]() ||
                        this.Frame[FrameState.Mouse_Is_Right_Pressed]())
                    {
                        return buttonSettings.BoundaryMousePressedColor;
                    }
                    else if (this.Frame[FrameState.Mouse_Is_Over]())
                    {
                        return buttonSettings.BoundaryMouseOverColor;
                    }

                    return buttonSettings.BoundaryRegularColor;
                },
                () => false);

            this.MouseLeftPressedAction        = () => {};
            this.MouseLeftDoubleClickedAction  = () => {};
            this.MouseRightPressedAction       = () => {};
            this.MouseRightDoubleClickedAction = () => {};

            this.Frame.MousePressLeft        += (sender, args) => this.MouseLeftPressedAction();
            this.Frame.MouseDoubleClickLeft  += (sender, args) => this.MouseLeftDoubleClickedAction();
            this.Frame.MousePressRight       += (sender, args) => this.MouseRightPressedAction();
            this.Frame.MouseDoubleClickRight += (sender, args) => this.MouseRightDoubleClickedAction();
        }

        public Label Label { get; set; }

        private Box BoxFilled { get; set; }

        private Box BoxBoundary { get; set; }

        private PickableFrame Frame { get; set; }

        public Action MouseLeftPressedAction { get; set; }

        public Action MouseLeftDoubleClickedAction { get; set; }

        public Action MouseRightPressedAction { get; set; }

        public Action MouseRightDoubleClickedAction { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.BoxBoundary.Draw(graphics, time, alpha);
            this.BoxFilled  .Draw(graphics, time, alpha);
            this.Label      .Draw(graphics, time, alpha);
            base            .Draw(graphics, time, alpha);
        }

        public override void Update(GameTime time)
        {
            this.Frame.Update(time);
            base      .Update(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.Frame.UpdateInput(input, time);
            base      .UpdateInput(input, time);
        }
    }
}