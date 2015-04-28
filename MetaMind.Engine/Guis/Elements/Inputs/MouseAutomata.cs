namespace MetaMind.Engine.Guis.Elements.Inputs
{
    /// <summary>
    /// Implemented as a pushdown automata
    /// </summary>
    internal class MouseAutomata
    {
        private ButtonHistory lbutton = new ButtonHistory();

        private PositionHistory position = new PositionHistory();

        private ButtonHistory rbutton = new ButtonHistory();

        public bool IsLButtonDoubleClicked
        {
            get
            {
                return this.IsButtonDoubleClicked(this.lbutton);
            }
        }

        public bool IsLButtonPressed
        {
            get
            {
                return this.IsButtonPressed(this.lbutton);
            }
        }

        public bool IsLButtonReleased
        {
            get
            {
                return this.IsButtonReleased(this.lbutton);
            }
        }

        public bool IsMouseOver
        {
            get
            {
                return this.position.Count != 0 && this.position.Peek() is MouseOver;
            }
        }

        public bool IsRButtonDoubleClicked
        {
            get
            {
                return this.IsButtonDoubleClicked(this.rbutton);
            }
        }

        public bool IsRButtonPressed
        {
            get
            {
                return this.IsButtonPressed(this.rbutton);
            }
        }

        public bool IsRButtonReleased
        {
            get
            {
                return this.IsButtonReleased(this.rbutton);
            }
        }

        public void Enter()
        {
            this.position.Push(new MouseOver());
        }

        public void LClear()
        {
            this.Clear(this.lbutton);
        }

        public void LDoubleClick()
        {
            this.DoubleClick(this.lbutton, this.IsMouseOver);
        }

        public void Leave()
        {
            this.position.Pop();
        }

        public void LPress()
        {
            this.Press(this.lbutton, this.IsMouseOver);
        }

        public void LRelease()
        {
            this.Release(this.lbutton);
        }

        public void RClear()
        {
            this.Clear(this.rbutton);
        }

        public void RDoubleClick()
        {
            this.DoubleClick(this.rbutton, this.IsMouseOver);
        }

        public void RPress()
        {
            this.Press(this.rbutton, this.IsMouseOver);
        }

        public void RRelease()
        {
            this.Release(this.rbutton);
        }

        protected void Clear(ButtonHistory button)
        {
            button.Clear();
        }

        protected void DoubleClick(ButtonHistory button, bool isMouseOver)
        {
            this.Clear(button);
            button.Push(new ButtonDoubleClick(isMouseOver));
        }

        protected void Press(ButtonHistory button, bool isMouseOver)
        {
            button.Push(new ButtonPress(isMouseOver));
        }

        protected void Release(ButtonHistory button)
        {
            if (button.Count != 0)
            {
                button.Pop();
            }
        }

        private bool IsButtonDoubleClicked(ButtonHistory button)
        {
            return button.Count != 0 && button.Peek() is ButtonDoubleClick;
        }

        private bool IsButtonPressed(ButtonHistory button)
        {
            return button.Count != 0 && button.Peek() is ButtonPress;
        }

        private bool IsButtonReleased(ButtonHistory button)
        {
            return button.Count == 0;
        }
    }
}