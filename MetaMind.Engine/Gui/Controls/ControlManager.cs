namespace MetaMind.Engine.Gui.Controls
{
    using Microsoft.Xna.Framework;
    using Reactors;

    public class ControlManager : ControlReactor
    {
        #region Constructors

        public ControlManager()
        {
        }

        #endregion

        #region Focus Data

        private Control focusedControl = null;

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

                            this.focusedControl = value.Root;
                        }
                        else if (this.focusedControl == null)
                        {
                            this.focusedControl = value.Root;
                        }
                    }
                }
                else if (value == null)
                {
                    this.focusedControl = null;
                }
            }
        }

        #endregion


        #region Control Data

        public ControlCollection Controls { get; protected set; } = new ControlCollection();

        /// <summary>
        /// Adds control into control list. This method serves different purpose 
        /// from base.Add(RenderComponent), because // TODO(Critical): I don't really know how to design the purpose of control tree.
        /// </summary>
        public void Add(Control control)
        {
            if (control != null)
            {
                if (!this.Children.Contains(control))
                {
                    // Restore parent relationship before adding
                    control.Parent?.Remove(control);

                    // Configure parenthood
                    control.Enabled = (this.Enabled ? control.Enabled : this.Enabled);
                    control.Parent = this;
                    control.Root = this.Root;

                    // Add to children list
                    this.Children.Add(control);
                }
            }
        }

        /// <summary>
        /// Remove existing parenthood to original state.
        /// </summary>
        /// <param name="control"></param>
        public void Remove(Control control)
        {
            if (control != null)
            {
                // Remove from children list
                this.Children.Remove(control);

                // Reconfigure parenthood to original state
                control.Parent = null;
                control.Root = control;
            }
        }

        public bool Contains(Control control, bool recursive)
        {
            if (this.Children != null)
            {
                foreach (var c in this.Children)
                {
                    if (c == control)
                    {
                        return true;
                    }

                    if (recursive && c.Contains(control, true))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion


        public override void Update(GameTime time)
        {
            base.Update(time);
        }
    }
}
