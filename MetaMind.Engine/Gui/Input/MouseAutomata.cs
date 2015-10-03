namespace MetaMind.Engine.Gui.Input
{
    /// <summary>
    /// Implemented as a pushdown automata
    /// </summary>
    internal class MouseAutomata
    {
        private MouseButtonHistory lbutton = new MouseButtonHistory();

        private MousePositionHistory position = new MousePositionHistory();

        private MouseButtonHistory rbutton = new MouseButtonHistory();

        #region States

        public bool IsLButtonDoubleClicked => this.IsButtonDoubleClicked(this.lbutton);

        public bool IsLButtonPressed => this.IsButtonPressed(this.lbutton);

        public bool IsLButtonReleased => this.IsButtonReleased(this.lbutton);

        public bool IsMouseOver => this.position.Count != 0 && this.position.Peek() is MouseOver;

        public bool IsRButtonDoubleClicked => this.IsButtonDoubleClicked(this.rbutton);

        public bool IsRButtonPressed => this.IsButtonPressed(this.rbutton);

        public bool IsRButtonReleased => this.IsButtonReleased(this.rbutton);

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
            this.Clear(this.lbutton);
        }

        public void RClear()
        {
            this.Clear(this.rbutton);
        }

        protected void DoubleClick(MouseButtonHistory button, bool isMouseOver)
        {
            this.Clear(button);
            button.Push(new MouseButtonDoubleClick(isMouseOver));
        }

        public void LDoubleClick()
        {
            this.DoubleClick(this.lbutton, this.IsMouseOver);
        }

        public void RDoubleClick()
        {
            this.DoubleClick(this.rbutton, this.IsMouseOver);
        }

        public void Enter()
        {
            this.position.Push(new MouseOver());
        }

        public void Leave()
        {
            this.position.Pop();
        }

        protected void Press(MouseButtonHistory button, bool isMouseOver)
        {
            button.Push(new MouseButtonPress(isMouseOver));
        }

        public void LPress()
        {
            this.Press(this.lbutton, this.IsMouseOver);
        }

        public void RPress()
        {
            this.Press(this.rbutton, this.IsMouseOver);
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
            this.Release(this.lbutton);
        }

        public void RRelease()
        {
            this.Release(this.rbutton);
        }

        #endregion
    }
}