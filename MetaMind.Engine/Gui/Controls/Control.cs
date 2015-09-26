namespace MetaMind.Engine.Gui.Controls
{
    using System;

    public class Control : ControlComponent, IControl
    {
        public Control(ControlManager manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager), @"Control cannot be created. Manager instance is needed.");
            }

            this.Manager = manager;
            
        }

        #region Manager

        public ControlManager Manager { get; set; } = null;

        public bool Managed => this.Manager != null;

        #endregion

        public virtual bool Focused
        {
            get
            {
                return this.Manager.FocusedControl == this;
            }
            set
            {
                var changed = this.Focused != value;

                if (value)
                {
                    this.Manager.FocusedControl = this;

                    if (this.Active && changed)
                    {
                        this.OnFocusGained(EventArgs.Empty);
                    }
                }
                else
                {
                    if (this.Manager.FocusedControl == this)
                    {
                        this.Manager.FocusedControl = null;
                    }

                    if (this.Active && changed)
                    {
                        this.OnFocusLost(EventArgs.Empty);
                    }
                }
            }
        }

        public virtual bool CanFocus { get; set; } = true;

        #region Events

        public event EventHandler FocusLost = delegate { };

        public event EventHandler FocusGained = delegate { };

        #endregion

        #region Event On

        protected virtual void OnFocusLost(EventArgs e)
        {
            this.FocusLost?.Invoke(this, e);
        }

        protected virtual void OnFocusGained(EventArgs e)
        {
            this.FocusGained?.Invoke(this, e);
        }

        #endregion
    }
}