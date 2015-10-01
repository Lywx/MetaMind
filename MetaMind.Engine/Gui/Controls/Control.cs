namespace MetaMind.Engine.Gui.Controls
{
    using System;
    using Entities;
    using Microsoft.Xna.Framework;
    using Reactors;
    using Service;

    public class Control : ControlReactor, IControl
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

        #endregion

        #region Render Data

        public bool IsRenderChild => base.IsChild;

        public bool IsRenderParent => base.IsParent;

        public RenderReactor RenderRoot
        {
            get { return base.Root; }
            protected set { base.Root = value; }
        }

        public RenderReactor RenderParent
        {
            get { return base.Parent; }
            protected set { base.Parent = value; }
        }

        public RenderReactorCollection RenderChildren
        {
            get { return base.Children; }
            protected set { base.Children = value; }
        }

        #endregion

        #region Control Data

        public new ControlCollection Children { get; protected set; } = new ControlCollection();

        public new Control Parent { get; protected set; }

        public new Control Root { get; protected set; }

        public new bool IsChild => this.Parent != null;

        public new bool IsParent => this.Children != null && this.Children.Count > 0;

        public new bool IsRoot => this.Root == this;

        /// <summary>
        /// Adds control into control list. This method serves different purpose 
        /// from base.Add(RenderComponent), because // TODO(Critical): I don't really know how to design the purpose of control tree.
        /// </summary>
        public virtual void Add(Control control)
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
        public virtual void Remove(Control control)
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

        public virtual bool Contains(Control control, bool recursive)
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

        #region Minion Data

        /// <summary>
        /// The collection that contains 
        /// </summary>
        public MMEntityCollection<IMMEntity> Minions { get; } = new MMEntityCollection<IMMEntity>();

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

        #region Update

        public override void Update(GameTime time)
        {
            if (!this.Enabled)
            {
                return;
            }

            base         .Update(time);
            this.Children.Update(time);
            this.Minions .Update(time);
        }

        public override void UpdateInput(IMMEngineInputService input, GameTime time)
        {
            if (!this.Enabled)
            {
                return;
            }

            base         .UpdateInput(input, time);
            this.Children.UpdateInput(input, time);
            this.Minions .UpdateInput(input, time);
        }

        #endregion

        #region Buffer

        public override void UpdateForwardBuffer()
        {
            if (!this.Enabled)
            {
                return;
            }

            base         .UpdateForwardBuffer();
            //TODO(Major): Shoud I DO this in chidllren
            this.Children.UpdateForwardBuffer();
            this.Minions .UpdateForwardBuffer();
        }

        public override void UpdateBackwardBuffer()
        {
            if (!this.Enabled)
            {
                return;
            }

            base         .UpdateBackwardBuffer();
            this.Children.UpdateBackwardBuffer();
            this.Minions .UpdateBackwardBuffer();
        }

        #endregion
    }
}