namespace MetaMind.Engine.Gui.Controls
{
    using System;
    using Entities;
    using Microsoft.Xna.Framework;
    using Service;

    public class MMControlComponent : MMInputEntity, IMMControlComponent, IMMControlComponentInternal
    {
        public MMControlComponent(MMControlManager manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager), @"Control cannot be created. Manager instance is needed.");
            }

            this.Manager = manager;
        }

        #region Manager

        public IMMControlManagerInternal Manager { get; set; } = null;

        IMMControlManager IMMControlComponent.Manager => this.Manager as IMMControlManager;

        #endregion

        #region State Data

        public bool Active { get; set; } = true;

        #endregion

        #region Control Data

        public virtual bool CanFocus { get; set; } = true;

        public virtual bool HasFocus
        {
            get
            {
                return this.Manager.FocusedComponent == this;
            }
            set
            {
                var changed = this.HasFocus != value;

                if (value)
                {
                    this.Manager.FocusedComponent = this;

                    if (this.Active && changed)
                    {
                        this.OnFocusGained(EventArgs.Empty);
                    }
                }
                else
                {
                    if (this.Manager.FocusedComponent == this)
                    {
                        this.Manager.FocusedComponent = null;
                    }

                    if (this.Active && changed)
                    {
                        this.OnFocusLost(EventArgs.Empty);
                    }
                }
            }
        }

        #endregion

        #region Organization Data

        public MMControlCollection Children { get; set; } = new MMControlCollection();

        public IMMControlComponent Parent { get; set; }

        public IMMControlComponent Root { get; set; }

        public bool IsChild => this.Parent != null;

        public bool IsParent => this.Children != null && this.Children.Count > 0;

        public bool IsRoot => this.Root == this;

        public virtual void Add(IMMControlComponentInternal component)
        {
            if (component != null)
            {
                if (!this.Children.Contains(component as IMMControlComponent))
                {
                    // Restore parent relationship before adding
                    component.Parent?.Remove(component);

                    // Configure parenthood
                    component.Enabled = (this.Enabled ? component.Enabled : this.Enabled);
                    component.Parent = this;
                    component.Root = this.Root;

                    // Add to children list
                    this.Children.Add(component as IMMControlComponent);
                }
            }
        }

        /// <summary>
        /// Remove existing parenthood to original state.
        /// </summary>
        public virtual void Remove(IMMControlComponentInternal component)
        {
            if (component != null)
            {
                // Remove from children list
                this.Children.Remove(component as IMMControlComponent);

                // Reconfigure parenthood to original state
                component.Parent = null;
                component.Root = component as IMMControlComponent;
            }
        }

        public virtual bool Contains(IMMControlComponentInternal component, bool recursive)
        {
            if (this.Children != null)
            {
                foreach (var c in this.Children)
                {
                    if (c == component)
                    {
                        return true;
                    }

                    if (recursive && c.Contains(component, true))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion

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

        #region Initialization

        public bool Initialized { get; private set; }

        public virtual void Initialize()
        {
            this.Initialized = true;
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
        }

        public override void UpdateInput(IMMEngineInputService input, GameTime time)
        {
            if (!this.Enabled)
            {
                return;
            }

            base         .UpdateInput(input, time);
            this.Children.UpdateInput(input, time);
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
            this.Children.UpdateForwardBuffer();
        }

        public override void UpdateBackwardBuffer()
        {
            if (!this.Enabled)
            {
                return;
            }

            base         .UpdateBackwardBuffer();
            this.Children.UpdateBackwardBuffer();
        }

        #endregion
    }
}