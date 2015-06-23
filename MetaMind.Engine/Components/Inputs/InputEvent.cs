// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Event.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Inputs
{
    using System;
    using System.Runtime.InteropServices;

    using MetaMind.Engine.Guis.Elements.Inputs;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class InputEvent : GameControllableComponent, IInputEvent
    {
        #region Windows Message Handler

        private IntPtr hIMC;

        private IntPtr wndProc;

        private WndProc hookProcHandler;

        private delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        #endregion Windows Message Handler

        #region Constructors

        public InputEvent(GameEngine engine) 
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException("engine");
            }

            var window = engine.Window;

            // This handler has to be a field that not garbage collected during the running of GameEngine.
            this.hookProcHandler = new WndProc(this.HookProc);

            // Register hook API handler
            this.wndProc = (IntPtr)SetWindowLong(window.Handle, GWL_WNDPROC, (int)Marshal.GetFunctionPointerForDelegate(this.hookProcHandler));

            // Register text input handler
            window.TextInput += (sender, args) => this.OnCharEntered(args);

            this.hIMC = ImmGetContext(window.Handle);
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Event raised when a character has been entered.
        /// </summary>
        public event EventHandler<CharEnteredEventArgs> CharEntered = delegate { };

        /// <summary>
        /// Event raised when a key has been pressed down. May fire multiple times due to keyboard repeat.
        /// </summary>
        public event EventHandler<KeyEventArgs> KeyDown = delegate { };

        /// <summary>
        /// Event raised when a key has been released.
        /// </summary>
        public event EventHandler<KeyEventArgs> KeyUp = delegate { };

        /// <summary>
        /// Event raised when a mouse button has been double clicked.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseDoubleClick = delegate { };

        /// <summary>
        /// Event raised when a mouse button is pressed.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseDown = delegate { };

        /// <summary>
        /// Event raised when the mouse has hovered in the same location for a short period of time.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseHover = delegate { };

        /// <summary>
        /// Event raised when the mouse changes location.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseMove = delegate { };

        /// <summary>
        /// Event raised when a mouse button is released.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseUp = delegate { };

        /// <summary>
        /// Event raised when the mouse wheel has been moved.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseWheel = delegate { };

        private void OnCharEntered(TextInputEventArgs args)
        {
            if (this.CharEntered != null)
            {
                this.CharEntered(null, new CharEnteredEventArgs(args.Character));
            }
        }

        private void OnMouseDoubleClick(MouseButton button, int wParam, int lParam)
        {
            if (this.MouseDoubleClick != null)
            {
                short x, y;
                this.MouseLocationFromLParam(lParam, out x, out y);

                this.MouseDoubleClick(null, new MouseEventArgs(button, 1, x, y, 0));
            }
        }

        private void OnMouseDown(MouseButton button, int wParam, int lParam)
        {
            if (this.MouseDown != null)
            {
                short x, y;
                this.MouseLocationFromLParam(lParam, out x, out y);

                this.MouseDown(null, new MouseEventArgs(button, 1, x, y, 0));
            }
        }

        private void OnMouseUp(MouseButton button, int wParam, int lParam)
        {
            if (this.MouseUp != null)
            {
                short x, y;
                this.MouseLocationFromLParam(lParam, out x, out y);

                this.MouseUp(null, new MouseEventArgs(button, 1, x, y, 0));
            }
        }

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

        [DllImport("user32.dll")]
        private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("Imm32.dll")]
        private static extern IntPtr ImmAssociateContext(IntPtr hWnd, IntPtr hIMC);

        [DllImport("Imm32.dll")]
        private static extern IntPtr ImmGetContext(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        #endregion DLL Imports

        #region Hook Proc

        private IntPtr HookProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            var returnCode = CallWindowProc(this.wndProc, hWnd, msg, wParam, lParam);

            // halt when input count exceeds
            if (!this.Controllable)
            {
                return returnCode;
            }

            switch (msg)
            {
                case WM_GETDLGCODE:
                    returnCode = (IntPtr)(returnCode.ToInt32() | DLGC_WANTALLKEYS);
                    break;

                case WM_KEYDOWN:
                    if (this.KeyDown != null)
                    {
                        this.KeyDown(null, new KeyEventArgs((Keys)wParam));
                    }

                    break;

                case WM_KEYUP:
                    if (this.KeyUp != null)
                    {
                        this.KeyUp(null, new KeyEventArgs((Keys)wParam));
                    }

                    break;

                // Since the new MonoGame handle the IME input by Game.Window.TextInput event, I think this won't work for IME anymore. 
                // It may still work with ASCII but that is also included in Game.Window.TextInput.
                case WM_CHAR:
                    break;

                case WM_IME_SETCONTEXT:
                    if (wParam.ToInt32() == 1)
                    {
                        ImmAssociateContext(hWnd, this.hIMC);
                    }

                    break;

                // Language Change
                case WM_INPUTLANGCHANGE:
                    ImmAssociateContext(hWnd, this.hIMC);
                    returnCode = (IntPtr)1;
                    break;

                // Mouse messages
                case WM_MOUSEMOVE:
                    if (this.MouseMove != null)
                    {
                        short x, y;
                        this.MouseLocationFromLParam(lParam.ToInt32(), out x, out y);

                        this.MouseMove(null, new MouseEventArgs(MouseButton.None, 0, x, y, 0));
                    }

                    break;

                case WM_MOUSEHOVER:
                    if (this.MouseHover != null)
                    {
                        short x, y;
                        this.MouseLocationFromLParam(lParam.ToInt32(), out x, out y);

                        this.MouseHover(null, new MouseEventArgs(MouseButton.None, 0, x, y, 0));
                    }

                    break;

                case WM_MOUSEWHEEL:
                    if (this.MouseWheel != null)
                    {
                        short x, y;
                        this.MouseLocationFromLParam(lParam.ToInt32(), out x, out y);

                        this.MouseWheel(null, new MouseEventArgs(MouseButton.None, 0, x, y, (wParam.ToInt32() >> 16) / 120));
                    }

                    break;

                case WM_LBUTTONDOWN:
                    this.OnMouseDown(MouseButton.Left, wParam.ToInt32(), lParam.ToInt32());
                    break;

                case WM_LBUTTONUP:
                    this.OnMouseUp(MouseButton.Left, wParam.ToInt32(), lParam.ToInt32());
                    break;

                case WM_LBUTTONDBLCLK:
                    this.OnMouseDoubleClick(MouseButton.Left, wParam.ToInt32(), lParam.ToInt32());
                    break;

                case WM_RBUTTONDOWN:
                    this.OnMouseDown(MouseButton.Right, wParam.ToInt32(), lParam.ToInt32());
                    break;

                case WM_RBUTTONUP:
                    this.OnMouseUp(MouseButton.Right, wParam.ToInt32(), lParam.ToInt32());
                    break;

                case WM_RBUTTONDBLCLK:
                    this.OnMouseDoubleClick(MouseButton.Right, wParam.ToInt32(), lParam.ToInt32());
                    break;

                case WM_MBUTTONDOWN:
                    this.OnMouseDown(MouseButton.Middle, wParam.ToInt32(), lParam.ToInt32());
                    break;

                case WM_MBUTTONUP:
                    this.OnMouseUp(MouseButton.Middle, wParam.ToInt32(), lParam.ToInt32());
                    break;

                case WM_MBUTTONDBLCLK:
                    this.OnMouseDoubleClick(MouseButton.Middle, wParam.ToInt32(), lParam.ToInt32());
                    break;

                case WM_XBUTTONDOWN:
                    if ((wParam.ToInt32() & 0x10000) != 0)
                    {
                        this.OnMouseDown(MouseButton.X1, wParam.ToInt32(), lParam.ToInt32());
                    }
                    else if ((wParam.ToInt32() & 0x20000) != 0)
                    {
                        this.OnMouseDown(MouseButton.X2, wParam.ToInt32(), lParam.ToInt32());
                    }

                    break;

                case WM_XBUTTONUP:
                    if ((wParam.ToInt32() & 0x10000) != 0)
                    {
                        this.OnMouseUp(MouseButton.X1, wParam.ToInt32(), lParam.ToInt32());
                    }
                    else if ((wParam.ToInt32() & 0x20000) != 0)
                    {
                        this.OnMouseUp(MouseButton.X2, wParam.ToInt32(), lParam.ToInt32());
                    }

                    break;

                case WM_XBUTTONDBLCLK:
                    if ((wParam.ToInt32() & 0x10000) != 0)
                    {
                        this.OnMouseDoubleClick(MouseButton.X1, wParam.ToInt32(), lParam.ToInt32());
                    }
                    else if ((wParam.ToInt32() & 0x20000) != 0)
                    {
                        this.OnMouseDoubleClick(MouseButton.X2, wParam.ToInt32(), lParam.ToInt32());
                    }

                    break;
            }

            return returnCode;
        }

        #endregion

        #region Window Proc

        private void MouseLocationFromLParam(int lParam, out short x, out short y)
        {
            // Cast to signed shorts to get sign extension on negative coordinates (of course this would only be possible if mouse capture was enabled).
            x = (short)(lParam & 0xFFFF);
            y = (short)(lParam >> 16);
        }

        #endregion 

        #region Update

        public override void UpdateInput(GameTime gameTime)
        {
        }

        #endregion
    }
}