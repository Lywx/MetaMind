﻿namespace MetaMind.Engine.Guis.Elements
{
    using System;

    using MetaMind.Engine.Components.Inputs;

    using Microsoft.Xna.Framework;

    public class PickableFrame : PressableFrame, IPickableFrame
    {
        #region Constructors and Destructors

        public PickableFrame(Rectangle rectangle)
            : this()
        {
            this.Populate(rectangle);
        }

        public PickableFrame()
        {
            this.InputEvent.MouseDoubleClick += this.DetectMouseLeftDoubleClick;
            this.InputEvent.MouseDoubleClick += this.DetectMouseRightDoubleClick;
        }

        ~PickableFrame()
        {
            this.Dispose();
        }

        public override void Dispose()
        {
            this.MouseLeftClicked         = null;
            this.MouseLeftClickedOutside  = null;
            this.MouseLeftDoubleClicked   = null;
            this.MouseRightClicked        = null;
            this.MouseRightClickedOutside = null;
            this.MouseRightDoubleClicked  = null;

            this.InputEvent.MouseDoubleClick -= this.DetectMouseLeftDoubleClick;
            this.InputEvent.MouseDoubleClick -= this.DetectMouseRightDoubleClick;

            base.Dispose();
        }

        #endregion Constructors

        #region Events

        public event EventHandler<FrameEventArgs> MouseLeftClicked;

        public event EventHandler<FrameEventArgs> MouseLeftClickedOutside;

        public event EventHandler<FrameEventArgs> MouseLeftDoubleClicked;

        public event EventHandler<FrameEventArgs> MouseRightClicked;

        public event EventHandler<FrameEventArgs> MouseRightClickedOutside;

        public event EventHandler<FrameEventArgs> MouseRightDoubleClicked;

        protected override void DetectMouseLeftPressed(object sender, MouseEventArgs e)
        {
            base.DetectMouseLeftPressed(sender, e);

            if (this[FrameState.Mouse_Over]() && this.MouseLeftPress(e))
            {
                this[FrameState.Mouse_Left_Clicked_Outside] = () => false;
                this[FrameState.Mouse_Left_Double_Clicked] = () => false;
                this[FrameState.Mouse_Right_Clicked] = () => false;
                this[FrameState.Mouse_Right_Clicked_Outside] = () => false;
                this[FrameState.Mouse_Right_Double_Clicked] = () => false;

                this[FrameState.Mouse_Left_Clicked] = () => true;
                if (this.MouseLeftClicked != null)
                {
                    this.MouseLeftClicked(this, new FrameEventArgs(FrameEventType.Mouse_Left_Clicked));
                }
            }
            else if (!this[FrameState.Mouse_Over]() && this.MouseLeftPress(e))
            {
                this[FrameState.Mouse_Left_Clicked] = () => false;
                this[FrameState.Mouse_Left_Double_Clicked] = () => false;
                this[FrameState.Mouse_Right_Clicked] = () => false;
                this[FrameState.Mouse_Right_Clicked_Outside] = () => false;
                this[FrameState.Mouse_Right_Double_Clicked] = () => false;

                this[FrameState.Mouse_Left_Clicked_Outside] = () => true;

                if (this.MouseLeftClickedOutside != null)
                {
                    this.MouseLeftClickedOutside(this, new FrameEventArgs(FrameEventType.Mouse_Left_Clicked_Outside));
                }
            }
        }

        protected override void DetectMouseRightPressed(object sender, MouseEventArgs e)
        {
            base.DetectMouseRightPressed(sender, e);

            if (this[FrameState.Mouse_Over]() && this.MouseRightPress(e))
            {
                this[FrameState.Mouse_Left_Clicked] = () => false;
                this[FrameState.Mouse_Left_Clicked_Outside] = () => false;
                this[FrameState.Mouse_Left_Double_Clicked] = () => false;
                this[FrameState.Mouse_Right_Clicked_Outside] = () => false;
                this[FrameState.Mouse_Right_Double_Clicked] = () => false;

                this[FrameState.Mouse_Right_Clicked] = () => true;
                if (this.MouseRightClicked != null)
                {
                    this.MouseRightClicked(this, new FrameEventArgs(FrameEventType.Mouse_Right_Clicked));
                }
            }
            else if (!this[FrameState.Mouse_Over]() && this.MouseRightPress(e))
            {
                this[FrameState.Mouse_Left_Clicked] = () => false;
                this[FrameState.Mouse_Left_Clicked_Outside] = () => false;
                this[FrameState.Mouse_Left_Double_Clicked] = () => false;
                this[FrameState.Mouse_Right_Clicked] = () => false;
                this[FrameState.Mouse_Right_Double_Clicked] = () => false;

                this[FrameState.Mouse_Right_Clicked_Outside] = () => true;
                if (this.MouseRightClickedOutside != null)
                {
                    this.MouseRightClickedOutside(this, new FrameEventArgs(FrameEventType.Mouse_Right_Clicked_Outside));
                }
            }
        }

        private void DetectMouseLeftDoubleClick(object sender, MouseEventArgs e)
        {
            if (this[FrameState.Mouse_Over]() && this.MouseLeftPress(e))
            {
                this[FrameState.Mouse_Left_Clicked] = () => false;
                this[FrameState.Mouse_Right_Double_Clicked] = () => false;

                this[FrameState.Mouse_Left_Double_Clicked] = () => true;
                if (this.MouseLeftDoubleClicked != null)
                {
                    this.MouseLeftDoubleClicked(this, new FrameEventArgs(FrameEventType.Mouse_Left_Double_Clicked));
                }
            }
            else
            {
                this[FrameState.Mouse_Left_Clicked] = () => false;
                this[FrameState.Mouse_Left_Double_Clicked] = () => false;
            }
        }

        private void DetectMouseRightDoubleClick(object sender, MouseEventArgs e)
        {
            if (this[FrameState.Mouse_Over]() && this.MouseRightPress(e))
            {
                this[FrameState.Mouse_Right_Clicked] = () => false;
                this[FrameState.Mouse_Left_Clicked] = () => false;

                this[FrameState.Mouse_Right_Double_Clicked] = () => true;
                if (this.MouseRightDoubleClicked != null)
                {
                    this.MouseRightDoubleClicked(this, new FrameEventArgs(FrameEventType.Mouse_Right_Double_Clicked));
                }
            }
            else
            {
                this[FrameState.Mouse_Right_Clicked] = () => false;
                this[FrameState.Mouse_Right_Double_Clicked] = () => false;
            }
        }

        #endregion Events
    }
}