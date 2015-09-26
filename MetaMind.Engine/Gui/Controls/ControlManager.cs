namespace MetaMind.Engine.Gui.Controls
{
    public class ControlManager : GameControllableEntity
    {
        private Control focusedControl = null;

        public ControlManager()
        {
        }

        /// <summary>
        /// Returns currently focused control.
        /// </summary>
        public Control FocusedControl
        {
            get
            {
                return this.focusedControl;
            }

            internal set
            {
                if (value != null && 
                    value.Visible && 
                    value.Enabled)
                {
                    if (value.CanFocus)
                    {
                        // When current manager doesn't have focused control
                        if (this.focusedControl == null || 

                           // Or value is not the parent(or in the parent family) of current focused control
                           (this.focusedControl != null && value.Root != this.focusedControl.Root) || 

                           // Or value is not root control
                           !value.IsRoot)
                        {
                            if (this.focusedControl != null && this.focusedControl != value)
                            {
                                // Blur current focused control
                                this.focusedControl.Focused = false;
                            }

                            this.focusedControl = value;
                        }
                    }
                    else if (!value.CanFocus)
                    {
                        // When 
                        if (this.focusedControl != null && value.Root != this.focusedControl.Root)
                        {
                            if (this.focusedControl != value.Root)
                            {
                                this.focusedControl.Focused = false;
                            }

                            this.focusedControl = (Control)value.Root;
                        }
                        else if (this.focusedControl == null)
                        {
                            this.focusedControl = (Control)value.Root;
                        }
                    }
                }
                else if (value == null)
                {
                    this.focusedControl = null;
                }
            }
        }
    }
}
