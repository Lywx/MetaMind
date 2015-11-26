namespace MetaMind.Engine.Entities.Input
{
    /// <summary>
    /// Implemented as a pushdown automata.
    /// </summary>
    internal class MouseStateAutomata
    {
        private MouseButtonHistory lbuttonHistory = new MouseButtonHistory();

        private MouseButtonHistory rbuttonHistory = new MouseButtonHistory();

        private MousePositionHistory positionHistory = new MousePositionHistory();

        #region States

        public bool IsLButtonDoubleClicked => this.IsButtonDoubleClicked(this.lbuttonHistory);

        public bool IsLButtonPressed => this.IsButtonPressed(this.lbuttonHistory);

        public bool IsLButtonReleased => this.IsButtonReleased(this.lbuttonHistory);

        public bool IsMouseOver => this.positionHistory.Count != 0 && this.positionHistory.Peek() is MousePositionOver;

        public bool IsRButtonDoubleClicked => this.IsButtonDoubleClicked(this.rbuttonHistory);

        public bool IsRButtonPressed => this.IsButtonPressed(this.rbuttonHistory);

        public bool IsRButtonReleased => this.IsButtonReleased(this.rbuttonHistory);

        private bool IsButtonDoubleClicked(MouseButtonHistory button)
        {
            return button.Count != 0 && button.Peek() is MouseButtonDoubleClick;
        }

        private bool IsButtonPressed(MouseButtonHistory button)
        {
            return button.Count != 0 && button.Peek() is MouseButtonPress;
        }

        private bool IsButtonReleased(MouseButtonHistory button)
        {
            return button.Count == 0;
        }

        #endregion

        #region Operations

        protected void Clear(MouseButtonHistory button)
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

        protected void DoubleClick(MouseButtonHistory button, bool isMouseOver)
        {
            this.Clear(button);
            button.Push(new MouseButtonDoubleClick(isMouseOver));
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
            this.positionHistory.Push(new MousePositionOver());
        }

        public void Leave()
        {
            this.positionHistory.Pop();
        }

        protected void Press(MouseButtonHistory button, bool isMouseOver)
        {
            button.Push(new MouseButtonPress(isMouseOver));
        }

        public void LPress()
        {
            this.Press(this.lbuttonHistory, this.IsMouseOver);
        }

        public void RPress()
        {
            this.Press(this.rbuttonHistory, this.IsMouseOver);
        }

        protected void Release(MouseButtonHistory button)
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