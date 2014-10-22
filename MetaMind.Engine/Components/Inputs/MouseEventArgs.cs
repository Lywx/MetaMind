using System;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Components.Inputs
{
    public class MouseEventArgs : EventArgs
    {
        private MouseButton button;
        private int clicks;
        private int delta;
        private int x;
        private int y;

        public MouseEventArgs(MouseButton button, int clicks, int x, int y, int delta)
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
        public Point Location { get { return new Point(x, y); } }
        public int X { get { return x; } }

        public int Y { get { return y; } }
    }
}