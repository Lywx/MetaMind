namespace MetaMind.Engine.Gui.Controls.Images
{
    public struct Margin 
    {
        public int Left;

        public int Top;

        public int Right;

        public int Bottom;

        public int Vertical => (this.Top + this.Bottom);

        public int Horizontal => (this.Left + this.Right);

        public Margin(int left, int top, int right, int bottom)
        {
            this.Left   = left;
            this.Top    = top;
            this.Right  = right;
            this.Bottom = bottom;
        }
    }
}