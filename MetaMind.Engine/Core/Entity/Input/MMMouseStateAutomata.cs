namespace MetaMind.Engine.Core.Entity.Input
{
    /// <summary>
    /// Implemented as a pushdown automata.
    /// </summary>
    internal class MMMouseStateAutomata
    {
        private MMMouseButtonHistory lbuttonHistory = new MMMouseButtonHistory();

        private MMMouseButtonHistory rbuttonHistory = new MMMouseButtonHistory();

        private MMMousePositionHistory positionHistory = new MMMousePositionHistory();

        #region States

        public bool IsLButtonDoubleClicked => this.IsButtonDoubleClicked(this.lbuttonHistory);

        public bool IsLButtonPressed => this.IsButtonPressed(this.lbuttonHistory);

        public bool IsLButtonReleased => this.IsButtonReleased(this.lbuttonHistory);

        public bool IsMouseOver => this.positionHistory.Count != 0 && this.positionHistory.Peek() is MMMousePositionOver;

        public bool IsRButtonDoubleClicked => this.IsButtonDoubleClicked(this.rbuttonHistory);

        public bool IsRButtonPressed => this.IsButtonPressed(this.rbuttonHistory);

        public bool IsRButtonReleased => this.IsButtonReleased(this.rbuttonHistory);

        private bool IsButtonDoubleClicked(MMMouseButtonHistory button)
        {
            return button.Count != 0 && button.Peek() is MMMouseButtonDoubleClick;
        }

        private bool IsButtonPressed(MMMouseButtonHistory button)
        {
            return button.Count != 0 && button.Peek() is MMMouseButtonPress;
        }

        private bool IsButtonReleased(MMMouseButtonHistory button)
        {
            return button.Count == 0;
        }

        #endregion

        #region Operations

        protected void Clear(MMMouseButtonHistory button)
        {
            button.Clear();
        }

        public void LClear()
        {
            this.Clear(this.lbuttonHistory);
        }

        public void RClear()
        {
            this.Clear(this.rbuttonHistory);
        }

        protected void DoubleClick(MMMouseButtonHistory button, bool isMouseOver)
        {
            this.Clear(button);
            button.Push(new MMMouseButtonDoubleClick(isMouseOver));
        }

        public void LDoubleClick()
        {
            this.DoubleClick(this.lbuttonHistory, this.IsMouseOver);
        }

        public void RDoubleClick()
        {
            this.DoubleClick(this.rbuttonHistory, this.IsMouseOver);
        }

        public void Enter()
        {
            this.positionHistory.Push(new MMMousePositionOver());
        }

        public void Leave()
        {
            this.positionHistory.Pop();
        }

        protected void Press(MMMouseButtonHistory button, bool isMouseOver)
        {
            button.Push(new MMMouseButtonPress(isMouseOver));
        }

        public void LPress()
        {
            this.Press(this.lbuttonHistory, this.IsMouseOver);
        }

        public void RPress()
        {
            this.Press(this.rbuttonHistory, this.IsMouseOver);
        }

        protected void Release(MMMouseButtonHistory button)
        {
            if (button.Count != 0)
            {
                button.Pop();
            }
        }

        public void LRelease()
        {
            this.Release(this.lbuttonHistory);
        }

        public void RRelease()
        {
            this.Release(this.rbuttonHistory);
        }

        #endregion
    }
}