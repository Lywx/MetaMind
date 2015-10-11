namespace MetaMind.Engine.Entities.Controls
{
    public struct MMMargin 
    {
        public int Left;

        public int Top;

        public int Right;

        public int Bottom;

        public MMMargin(int left, int top, int right, int bottom)
        {
            this.Left   = left;
            this.Top    = top;
            this.Right  = right;
            this.Bottom = bottom;
        }

        /// <summary>
        /// Sum of vertical margin.
        /// </summary>
        public int Vertical => (this.Top + this.Bottom);

        /// <summary>
        /// Sum of horizontal margin.
        /// </summary>
        public int Horizontal => (this.Left + this.Right);
    }
}