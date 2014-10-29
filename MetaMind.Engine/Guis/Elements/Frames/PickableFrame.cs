using System;
using MetaMind.Engine.Components;
using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Extensions;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Elements.Frames
{
    public class PickableFrame : PressableFrame, IPickableFrame
    {
        #region Constructors

        public PickableFrame( Rectangle rectangle )
            : this()
        {
            Initialize( rectangle );
        }

        public PickableFrame()
        {
            InputEventManager.MouseDoubleClick += DetectMouseLeftDoubleClick;
            InputEventManager.MouseDoubleClick += DetectMouseRightDoubleClick;
        }

        #endregion Constructors

        #region Events

        public event EventHandler<FrameEventArgs> MouseLeftClicked;

        public event EventHandler<FrameEventArgs> MouseLeftClickedOutside;

        public event EventHandler<FrameEventArgs> MouseLeftDoubleClicked;

        public event EventHandler<FrameEventArgs> MouseRightClicked;

        public event EventHandler<FrameEventArgs> MouseRightClickedOutside;

        public event EventHandler<FrameEventArgs> MouseRightDoubleClicked;

        protected override void DetectMouseLeftPressed( object sender, MouseEventArgs e )
        {
            base.DetectMouseLeftPressed( sender, e );

            if ( IsEnabled( FrameState.Mouse_Over ) && MouseLeftPress( e ) )
            {
                Disable( FrameState.Mouse_Left_Clicked_Outside );
                Disable( FrameState.Mouse_Left_Double_Clicked );
                Disable( FrameState.Mouse_Right_Clicked );
                Disable( FrameState.Mouse_Right_Clicked_Outside );
                Disable( FrameState.Mouse_Right_Double_Clicked );

                Enable( FrameState.Mouse_Left_Clicked );
                if ( MouseLeftClicked != null )
                    MouseLeftClicked( this, new FrameEventArgs( FrameEventType.Mouse_Left_Clicked ) );
            }
            else if ( !IsEnabled( FrameState.Mouse_Over ) && MouseLeftPress( e ) )
            {
                Disable( FrameState.Mouse_Left_Clicked );
                Disable( FrameState.Mouse_Left_Double_Clicked );
                Disable( FrameState.Mouse_Right_Clicked );
                Disable( FrameState.Mouse_Right_Clicked_Outside );
                Disable( FrameState.Mouse_Right_Double_Clicked );

                Enable( FrameState.Mouse_Left_Clicked_Outside );

                if ( MouseLeftClickedOutside != null )
                    MouseLeftClickedOutside( this, new FrameEventArgs( FrameEventType.Mouse_Left_Clicked_Outside ) );
            }
        }

        protected override void DetectMouseRightPressed( object sender, MouseEventArgs e )
        {
            base.DetectMouseRightPressed( sender, e );

            if ( IsEnabled( FrameState.Mouse_Over ) && MouseRightPress( e ) )
            {
                Disable( FrameState.Mouse_Left_Clicked );
                Disable( FrameState.Mouse_Left_Clicked_Outside );
                Disable( FrameState.Mouse_Left_Double_Clicked );
                Disable( FrameState.Mouse_Right_Clicked_Outside );
                Disable( FrameState.Mouse_Right_Double_Clicked );

                Enable( FrameState.Mouse_Right_Clicked );
                if ( MouseRightClicked != null )
                    MouseRightClicked( this, new FrameEventArgs( FrameEventType.Mouse_Right_Clicked ) );
            }
            else if ( !IsEnabled( FrameState.Mouse_Over ) && MouseRightPress( e ) )
            {
                Disable( FrameState.Mouse_Left_Clicked );
                Disable( FrameState.Mouse_Left_Clicked_Outside );
                Disable( FrameState.Mouse_Left_Double_Clicked );
                Disable( FrameState.Mouse_Right_Clicked );
                Disable( FrameState.Mouse_Right_Double_Clicked );

                Enable( FrameState.Mouse_Right_Clicked_Outside );
                if ( MouseRightClickedOutside != null )
                    MouseRightClickedOutside( this, new FrameEventArgs( FrameEventType.Mouse_Right_Clicked_Outside ) );
            }
        }

        private void DetectMouseLeftDoubleClick( object sender, MouseEventArgs e )
        {
            if ( IsEnabled( FrameState.Mouse_Over ) && MouseLeftPress( e ) )
            {
                Disable( FrameState.Mouse_Left_Clicked );
                Disable( FrameState.Mouse_Right_Double_Clicked );

                Enable( FrameState.Mouse_Left_Double_Clicked );
                if ( MouseLeftDoubleClicked != null )
                    MouseLeftDoubleClicked( this, new FrameEventArgs( FrameEventType.Mouse_Left_Double_Clicked ) );
            }
            else
            {
                Disable( FrameState.Mouse_Left_Clicked );
                Disable( FrameState.Mouse_Left_Double_Clicked );
            }
        }

        private void DetectMouseRightDoubleClick( object sender, MouseEventArgs e )
        {
            if ( IsEnabled( FrameState.Mouse_Over ) && MouseRightPress( e ) )
            {
                Disable( FrameState.Mouse_Right_Clicked );
                Disable( FrameState.Mouse_Left_Clicked );

                Enable( FrameState.Mouse_Right_Double_Clicked );
                if ( MouseRightDoubleClicked != null )
                    MouseRightDoubleClicked( this, new FrameEventArgs( FrameEventType.Mouse_Right_Double_Clicked ) );
            }
            else
            {
                Disable( FrameState.Mouse_Right_Clicked );
                Disable( FrameState.Mouse_Right_Double_Clicked );
            }
        }

        #endregion Events
    }
}