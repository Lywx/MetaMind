﻿namespace MetaMind.Engine.Guis.Widgets.Regions
{
    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    using Stateless;

    public class Region : RegionEntity, IRegion
    {
        protected StateMachine<State, Trigger> StateMachine { get; private set; }

        public Region(Rectangle rectangle)
        {
            // State machine
            this.StateMachine = new StateMachine<State, Trigger>(State.LostFocus);

            this.StateMachine.Configure(State.LostFocus).PermitReentry(Trigger.PressedOutside);
            this.StateMachine.Configure(State.LostFocus).Permit(Trigger.PressedInside, State.HasFocus);

            this.StateMachine.Configure(State.HasFocus).PermitReentry(Trigger.PressedInside);
            this.StateMachine.Configure(State.HasFocus).Permit(Trigger.PressedOutside, State.LostFocus);

            // Frame events
            this.Frame = new PickableFrame(rectangle);

            this.Frame.MouseLeftPressed        += this.FrameMouseLeftPressed;
            this.Frame.MouseLeftPressedOutside += this.FrameMouseLeftPressedOutside;

            // Region states
            this[RegionState.Mouse_Is_Over] = this.Frame[FrameState.Mouse_Is_Over];

            this[RegionState.Region_Has_Focus] = () => this.StateMachine.IsInState(State.HasFocus);
        }

        protected enum State
        {
            HasFocus,

            LostFocus,
        }

        protected enum Trigger
        {
            PressedInside,

            PressedOutside,
        }

        public IPickableFrame Frame { get; set; }

        public int Height
        {
            get { return this.Frame.Height; }
            set { this.Frame.Height = value; }
        }

        public Point Location
        {
            get { return this.Frame.Location; }
            set { this.Frame.Location = value; }
        }

        public Rectangle Rectangle
        {
            get { return this.Frame.Rectangle; }
            set { this.Frame.Rectangle = value; }
        }

        public int Width
        {
            get { return this.Frame.Width; }
            set { this.Frame.Width = value; }
        }

        public int X
        {
            get { return this.Frame.X; }
            set { this.Frame.X = value; }
        }

        public int Y
        {
            get { return this.Frame.Y; }
            set { this.Frame.Y = value; }
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
        }

        private void FrameMouseLeftPressed(object sender, FrameEventArgs e)
        {
            this.StateMachine.Fire(Trigger.PressedInside);
        }

        private void FrameMouseLeftPressedOutside(object sender, FrameEventArgs e)
        {
            this.StateMachine.Fire(Trigger.PressedOutside);
        }
    }
}