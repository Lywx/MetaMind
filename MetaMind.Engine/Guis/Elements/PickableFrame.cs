namespace MetaMind.Engine.Guis.Elements
{
    using System;

    using MetaMind.Engine.Components;

    using Microsoft.Xna.Framework;

    public class PickableFrame : PressableFrame, IPickableFrame
    {
        #region Constructors and Destructors

        public PickableFrame(Rectangle rectangle)
            : this()
        {
            this.Initialize(rectangle);
        }

        public PickableFrame()
        {
            GameEngine.InputEventManager.MouseDoubleClick += this.DetectMouseLeftDoubleClick;
            GameEngine.InputEventManager.MouseDoubleClick += this.DetectMouseRightDoubleClick;
        }

        ~PickableFrame()
        {
            this.Dispose();
        }

        public override void Dispose()
        {
            try
            {
                this.MouseLeftClicked         = null;
                this.MouseLeftClickedOutside  = null;
                this.MouseLeftDoubleClicked   = null;
                this.MouseRightClicked        = null;
                this.MouseRightClickedOutside = null;
                this.MouseRightDoubleClicked  = null;

                GameEngine.InputEventManager.MouseDoubleClick -= this.DetectMouseLeftDoubleClick;
                GameEngine.InputEventManager.MouseDoubleClick -= this.DetectMouseRightDoubleClick;
            }
            finally
            {
                base.Dispose();
            }
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

            if (this.IsEnabled(FrameState.Mouse_Over) && this.MouseLeftPress(e))
            {
                this.Disable(FrameState.Mouse_Left_Clicked_Outside);
                this.Disable(FrameState.Mouse_Left_Double_Clicked);
                this.Disable(FrameState.Mouse_Right_Clicked);
                this.Disable(FrameState.Mouse_Right_Clicked_Outside);
                this.Disable(FrameState.Mouse_Right_Double_Clicked);

                this.Enable(FrameState.Mouse_Left_Clicked);
                if (this.MouseLeftClicked != null)
                {
                    this.MouseLeftClicked(this, new FrameEventArgs(FrameEventType.Mouse_Left_Clicked));
                }
            }
            else if (!this.IsEnabled(FrameState.Mouse_Over) && this.MouseLeftPress(e))
            {
                this.Disable(FrameState.Mouse_Left_Clicked);
                this.Disable(FrameState.Mouse_Left_Double_Clicked);
                this.Disable(FrameState.Mouse_Right_Clicked);
                this.Disable(FrameState.Mouse_Right_Clicked_Outside);
                this.Disable(FrameState.Mouse_Right_Double_Clicked);

                this.Enable(FrameState.Mouse_Left_Clicked_Outside);

                if (this.MouseLeftClickedOutside != null)
                {
                    this.MouseLeftClickedOutside(this, new FrameEventArgs(FrameEventType.Mouse_Left_Clicked_Outside));
                }
            }
        }

        protected override void DetectMouseRightPressed(object sender, MouseEventArgs e)
        {
            base.DetectMouseRightPressed(sender, e);

            if (this.IsEnabled(FrameState.Mouse_Over) && this.MouseRightPress(e))
            {
                this.Disable(FrameState.Mouse_Left_Clicked);
                this.Disable(FrameState.Mouse_Left_Clicked_Outside);
                this.Disable(FrameState.Mouse_Left_Double_Clicked);
                this.Disable(FrameState.Mouse_Right_Clicked_Outside);
                this.Disable(FrameState.Mouse_Right_Double_Clicked);

                this.Enable(FrameState.Mouse_Right_Clicked);
                if (this.MouseRightClicked != null)
                {
                    this.MouseRightClicked(this, new FrameEventArgs(FrameEventType.Mouse_Right_Clicked));
                }
            }
            else if (!this.IsEnabled(FrameState.Mouse_Over) && this.MouseRightPress(e))
            {
                this.Disable(FrameState.Mouse_Left_Clicked);
                this.Disable(FrameState.Mouse_Left_Clicked_Outside);
                this.Disable(FrameState.Mouse_Left_Double_Clicked);
                this.Disable(FrameState.Mouse_Right_Clicked);
                this.Disable(FrameState.Mouse_Right_Double_Clicked);

                this.Enable(FrameState.Mouse_Right_Clicked_Outside);
                if (this.MouseRightClickedOutside != null)
                {
                    this.MouseRightClickedOutside(this, new FrameEventArgs(FrameEventType.Mouse_Right_Clicked_Outside));
                }
            }
        }

        private void DetectMouseLeftDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.IsEnabled(FrameState.Mouse_Over) && this.MouseLeftPress(e))
            {
                this.Disable(FrameState.Mouse_Left_Clicked);
                this.Disable(FrameState.Mouse_Right_Double_Clicked);

                this.Enable(FrameState.Mouse_Left_Double_Clicked);
                if (this.MouseLeftDoubleClicked != null)
                {
                    this.MouseLeftDoubleClicked(this, new FrameEventArgs(FrameEventType.Mouse_Left_Double_Clicked));
                }
            }
            else
            {
                this.Disable(FrameState.Mouse_Left_Clicked);
                this.Disable(FrameState.Mouse_Left_Double_Clicked);
            }
        }

        private void DetectMouseRightDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.IsEnabled(FrameState.Mouse_Over) && this.MouseRightPress(e))
            {
                this.Disable(FrameState.Mouse_Right_Clicked);
                this.Disable(FrameState.Mouse_Left_Clicked);

                this.Enable(FrameState.Mouse_Right_Double_Clicked);
                if (this.MouseRightDoubleClicked != null)
                {
                    this.MouseRightDoubleClicked(this, new FrameEventArgs(FrameEventType.Mouse_Right_Double_Clicked));
                }
            }
            else
            {
                this.Disable(FrameState.Mouse_Right_Clicked);
                this.Disable(FrameState.Mouse_Right_Double_Clicked);
            }
        }

        #endregion Events
    }
}