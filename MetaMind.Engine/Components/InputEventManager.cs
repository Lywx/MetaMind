using System;
using System.Runtime.InteropServices;
using MetaMind.Engine.Guis.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MetaMind.Engine.Components
{
    public delegate void CharEnteredHandler( object sender, CharacterEventArgs e );

    public delegate void KeyEventHandler( object sender, KeyEventArgs e );

    public delegate void MouseEventHandler( object sender, MouseEventArgs e );

    public enum MouseButton
    {
        None,
        Left,
        Right,
        Middle,
        X1,
        X2
    }

    /// <summary>
    /// Mouse Key Flags from WinUser.h for mouse related WM messages.
    /// </summary>
    [Flags]
    public enum MouseKeys
    {
        LButton = 0x01,
        RButton = 0x02,
        Shift = 0x04,
        Control = 0x08,
        MButton = 0x10,
        XButton1 = 0x20,
        XButton2 = 0x40
    }

    public class CharacterEventArgs : EventArgs
    {
        private readonly byte[] character;
        private readonly int lParam;

        public CharacterEventArgs( byte[ ] character, int lParam )
        {
            this.character = character;
            this.lParam = lParam;
        }

        public bool AltPressed
        {
            get { return ( lParam & ( 1 << 29 ) ) > 0; }
        }

        public byte[ ] Character
        {
            get { return character; }
        }

        public bool ExtendedKey
        {
            get { return ( lParam & ( 1 << 24 ) ) > 0; }
        }

        public int Param
        {
            get { return lParam; }
        }

        public bool PreviousState
        {
            get { return ( lParam & ( 1 << 30 ) ) > 0; }
        }

        public int RepeatCount
        {
            get { return lParam & 0xffff; }
        }

        public bool TransitionState
        {
            get { return ( lParam & ( 1 << 31 ) ) > 0; }
        }
    }

    public class InputEventManager : Widget
    {
        #region Singleton

        private static InputEventManager singleton;

        public static InputEventManager GetInstance( Game game )
        {
            if ( singleton == null )
                singleton = new InputEventManager( game );
            return singleton;
        }

        #endregion Singleton

        #region Windows Message Handler

        private IntPtr hIMC;

        private WndProc hookProcHandler;

        private IntPtr prevWndProc;

        private delegate IntPtr WndProc( IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam );

        #endregion Windows Message Handler

        #region Constructors

        private bool isInitialized;

        private InputEventManager( Game game )
        {
            if ( !isInitialized )
                Initialize( game.Window );
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Event raised when a character has been entered.
        /// </summary>
        public event CharEnteredHandler CharEntered;

        /// <summary>
        /// Event raised when a key has been pressed down. May fire multiple times due to keyboard repeat.
        /// </summary>
        public event KeyEventHandler KeyDown;

        /// <summary>
        /// Event raised when a key has been released.
        /// </summary>
        public event KeyEventHandler KeyUp;

        /// <summary>
        /// Event raised when a mouse button has been double clicked.
        /// </summary>
        public event MouseEventHandler MouseDoubleClick;

        /// <summary>
        /// Event raised when a mouse button is pressed.
        /// </summary>
        public event MouseEventHandler MouseDown;

        /// <summary>
        /// Event raised when the mouse has hovered in the same location for a short period of time.
        /// </summary>
        public event MouseEventHandler MouseHover;

        /// <summary>
        /// Event raised when the mouse changes location.
        /// </summary>
        public event MouseEventHandler MouseMove;

        /// <summary>
        /// Event raised when a mouse button is released.
        /// </summary>
        public event MouseEventHandler MouseUp;

        /// <summary>
        /// Event raised when the mouse wheel has been moved.
        /// </summary>
        public event MouseEventHandler MouseWheel;

        #endregion Events

        #region Win32 Constants

        private const int DLGC_WANTALLKEYS = 4;
        private const int GWL_WNDPROC = -4;

        private const int WM_CHAR = 0x102;
        private const int WM_GETDLGCODE = 0x87;
        private const int WM_IME_COMPOSITION = 0x10F;
        private const int WM_IME_SETCONTEXT = 0x281;
        private const int WM_INPUTLANGCHANGE = 0x51;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_LBUTTONDBLCLK = 0x203;
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_LBUTTONUP = 0x202;
        private const int WM_MBUTTONDBLCLK = 0x209;
        private const int WM_MBUTTONDOWN = 0x207;
        private const int WM_MBUTTONUP = 0x208;
        private const int WM_MOUSEHOVER = 0x2A1;
        private const int WM_MOUSEMOVE = 0x200;
        private const int WM_MOUSEWHEEL = 0x20A;
        private const int WM_RBUTTONDBLCLK = 0x206;
        private const int WM_RBUTTONDOWN = 0x204;
        private const int WM_RBUTTONUP = 0x205;
        private const int WM_XBUTTONDBLCLK = 0x20D;
        private const int WM_XBUTTONDOWN = 0x20B;
        private const int WM_XBUTTONUP = 0x20C;

        #endregion Win32 Constants

        #region DLL Imports

        [DllImport( "user32.dll" )]
        private static extern IntPtr CallWindowProc( IntPtr lpPrevWndFunc, IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam );

        [DllImport( "Imm32.dll" )]
        private static extern IntPtr ImmAssociateContext( IntPtr hWnd, IntPtr hIMC );

        [DllImport( "Imm32.dll" )]
        private static extern IntPtr ImmGetContext( IntPtr hWnd );

        [DllImport( "user32.dll" )]
        private static extern int SetWindowLong( IntPtr hWnd, int nIndex, int dwNewLong );

        #endregion DLL Imports

        #region Initialization

        /// <summary>
        /// Initialize the TextInput with the given GameWindow.
        /// </summary>
        /// <param name="window">The XNA window to which text input should be linked.</param>
        public void Initialize( GameWindow window )
        {
            if ( isInitialized )
                throw new InvalidOperationException( "TextInput.Initialize can only be called once!" );

            hookProcHandler = HookProc;
            prevWndProc = ( IntPtr ) SetWindowLong( window.Handle, GWL_WNDPROC, ( int ) Marshal.GetFunctionPointerForDelegate( hookProcHandler ) );

            hIMC = ImmGetContext( window.Handle );
            isInitialized = true;
        }

        private IntPtr HookProc( IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam )
        {
            IntPtr returnCode = CallWindowProc( prevWndProc, hWnd, msg, wParam, lParam );

            if ( !IsHandlingInput )
                return returnCode;

            switch ( msg )
            {
                case WM_GETDLGCODE:
                    returnCode = ( IntPtr ) ( returnCode.ToInt32() | DLGC_WANTALLKEYS );
                    break;

                case WM_KEYDOWN:
                    if ( KeyDown != null )
                        KeyDown( null, new KeyEventArgs( ( Keys ) wParam ) );
                    break;

                case WM_KEYUP:
                    if ( KeyUp != null )
                        KeyUp( null, new KeyEventArgs( ( Keys ) wParam ) );
                    break;

                case WM_CHAR:
                    if ( CharEntered != null )
                    {
                        // convert wParam to byte for different IME encoding
                        var charBytes = BitConverter.GetBytes( ( int ) wParam );
                        CharEntered( null, new CharacterEventArgs( charBytes, lParam.ToInt32() ) );
                    }
                    break;

                case WM_IME_SETCONTEXT:
                    if ( wParam.ToInt32() == 1 )
                        ImmAssociateContext( hWnd, hIMC );
                    break;

                case WM_INPUTLANGCHANGE:
                    ImmAssociateContext( hWnd, hIMC );
                    returnCode = ( IntPtr ) 1;
                    break;

                // Mouse messages
                case WM_MOUSEMOVE:
                    if ( MouseMove != null )
                    {
                        short x, y;
                        MouseLocationFromLParam( lParam.ToInt32(), out x, out y );

                        MouseMove( null, new MouseEventArgs( MouseButton.None, 0, x, y, 0 ) );
                    }
                    break;

                case WM_MOUSEHOVER:
                    if ( MouseHover != null )
                    {
                        short x, y;
                        MouseLocationFromLParam( lParam.ToInt32(), out x, out y );

                        MouseHover( null, new MouseEventArgs( MouseButton.None, 0, x, y, 0 ) );
                    }
                    break;

                case WM_MOUSEWHEEL:
                    if ( MouseWheel != null )
                    {
                        short x, y;
                        MouseLocationFromLParam( lParam.ToInt32(), out x, out y );

                        MouseWheel( null, new MouseEventArgs( MouseButton.None, 0, x, y, ( wParam.ToInt32() >> 16 ) / 120 ) );
                    }
                    break;

                case WM_LBUTTONDOWN:
                    RaiseMouseDownEvent( MouseButton.Left, wParam.ToInt32(), lParam.ToInt32() );
                    break;

                case WM_LBUTTONUP:
                    RaiseMouseUpEvent( MouseButton.Left, wParam.ToInt32(), lParam.ToInt32() );
                    break;

                case WM_LBUTTONDBLCLK:
                    RaiseMouseDblClickEvent( MouseButton.Left, wParam.ToInt32(), lParam.ToInt32() );
                    break;

                case WM_RBUTTONDOWN:
                    RaiseMouseDownEvent( MouseButton.Right, wParam.ToInt32(), lParam.ToInt32() );
                    break;

                case WM_RBUTTONUP:
                    RaiseMouseUpEvent( MouseButton.Right, wParam.ToInt32(), lParam.ToInt32() );
                    break;

                case WM_RBUTTONDBLCLK:
                    RaiseMouseDblClickEvent( MouseButton.Right, wParam.ToInt32(), lParam.ToInt32() );
                    break;

                case WM_MBUTTONDOWN:
                    RaiseMouseDownEvent( MouseButton.Middle, wParam.ToInt32(), lParam.ToInt32() );
                    break;

                case WM_MBUTTONUP:
                    RaiseMouseUpEvent( MouseButton.Middle, wParam.ToInt32(), lParam.ToInt32() );
                    break;

                case WM_MBUTTONDBLCLK:
                    RaiseMouseDblClickEvent( MouseButton.Middle, wParam.ToInt32(), lParam.ToInt32() );
                    break;

                case WM_XBUTTONDOWN:
                    if ( ( wParam.ToInt32() & 0x10000 ) != 0 )
                    {
                        RaiseMouseDownEvent( MouseButton.X1, wParam.ToInt32(), lParam.ToInt32() );
                    }
                    else if ( ( wParam.ToInt32() & 0x20000 ) != 0 )
                    {
                        RaiseMouseDownEvent( MouseButton.X2, wParam.ToInt32(), lParam.ToInt32() );
                    }
                    break;

                case WM_XBUTTONUP:
                    if ( ( wParam.ToInt32() & 0x10000 ) != 0 )
                    {
                        RaiseMouseUpEvent( MouseButton.X1, wParam.ToInt32(), lParam.ToInt32() );
                    }
                    else if ( ( wParam.ToInt32() & 0x20000 ) != 0 )
                    {
                        RaiseMouseUpEvent( MouseButton.X2, wParam.ToInt32(), lParam.ToInt32() );
                    }
                    break;

                case WM_XBUTTONDBLCLK:
                    if ( ( wParam.ToInt32() & 0x10000 ) != 0 )
                    {
                        RaiseMouseDblClickEvent( MouseButton.X1, wParam.ToInt32(), lParam.ToInt32() );
                    }
                    else if ( ( wParam.ToInt32() & 0x20000 ) != 0 )
                    {
                        RaiseMouseDblClickEvent( MouseButton.X2, wParam.ToInt32(), lParam.ToInt32() );
                    }
                    break;
            }

            return returnCode;
        }

        #endregion Initialization

        #region Mouse Messages

        private void MouseLocationFromLParam( int lParam, out short x, out short y )
        {
            // Cast to signed shorts to get sign extension on negative coordinates (of course this would only be possible if mouse capture was enabled).
            x = ( short ) ( lParam & 0xFFFF );
            y = ( short ) ( lParam >> 16 );
        }

        private void RaiseMouseDblClickEvent( MouseButton button, int wParam, int lParam )
        {
            if ( MouseDoubleClick != null )
            {
                short x, y;
                MouseLocationFromLParam( lParam, out x, out y );

                MouseDoubleClick( null, new MouseEventArgs( button, 1, x, y, 0 ) );
            }
        }

        private void RaiseMouseDownEvent( MouseButton button, int wParam, int lParam )
        {
            if ( MouseDown != null )
            {
                short x, y;
                MouseLocationFromLParam( lParam, out x, out y );

                MouseDown( null, new MouseEventArgs( button, 1, x, y, 0 ) );
            }
        }

        private void RaiseMouseUpEvent( MouseButton button, int wParam, int lParam )
        {
            if ( MouseUp != null )
            {
                short x, y;
                MouseLocationFromLParam( lParam, out x, out y );

                MouseUp( null, new MouseEventArgs( button, 1, x, y, 0 ) );
            }
        }

        #endregion Mouse Messages

        #region Update and Draw

        public override void Draw(GameTime gameTime, byte alpha)
        {
        }

        public override void UpdateInput( GameTime gameTime )
        {
        }

        public override void UpdateStructure( GameTime gameTime )
        {
        }

        #endregion Update and Draw
    }

    public class KeyEventArgs : EventArgs
    {
        private Keys keyCode;

        public KeyEventArgs( Keys keyCode )
        {
            this.keyCode = keyCode;
        }

        public Keys KeyCode
        {
            get { return keyCode; }
        }
    }

    public class MouseEventArgs : EventArgs
    {
        private MouseButton button;
        private int clicks;
        private int delta;
        private int x;
        private int y;

        public MouseEventArgs( MouseButton button, int clicks, int x, int y, int delta )
        {
            this.button = button;
            this.clicks = clicks;
            this.x = x;
            this.y = y;
            this.delta = delta;
        }

        public MouseButton Button { get { return button; } }

        public int Clicks { get { return clicks; } }

        public int Delta { get { return delta; } }

        public Point Location { get { return new Point( x, y ); } }

        public int X { get { return x; } }

        public int Y { get { return y; } }
    }
}