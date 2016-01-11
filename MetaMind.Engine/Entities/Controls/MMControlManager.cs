namespace MetaMind.Engine.Entities.Controls
{
    using Bases;
    using Entities;
    using Microsoft.Xna.Framework;

    public class MMControlManager : MMInputEntity, IMMControlManager, IMMControlManagerInternal 
    {
        #region Constructors and Finalizer

        public MMControlManager()
        {
        }

        #endregion

        #region Focus Data

        private IMMControlComponent focusedComponent = null;

        /// <summary>
        /// Returns currently focused control.
        /// </summary>
        public IMMControlComponent FocusedComponent
        {
            get
            {
                return this.focusedComponent;
            }

            set
            {
                if (value != null && 
                    value.EntityEnabled)
                {
                    if (value.CanFocus)
                    {
                        // When current manager doesn't have focused control
                        if (this.focusedComponent == null || 

                           // Or value is not the parent(or in the parent family) of current focused control
                           (this.focusedComponent != null && value.Root != this.focusedComponent.Root) || 

                           // Or value is not root control
                           !value.IsRoot)
                        {
                            if (this.focusedComponent != null && this.focusedComponent != value)
                            {
                                // Blur current focused control
                                this.focusedComponent.HasFocus = false;
                            }

                            this.focusedComponent = value;
                        }
                    }
                    else if (!value.CanFocus)
                    {
                        // TODO: When 
                        if (this.focusedComponent != null
                            && value.Root != this.focusedComponent.Root)
                        {
                            if (this.focusedComponent != value.Root)
                            {
                                this.focusedComponent.HasFocus = false;
                            }

                            this.focusedComponent = value.Root;
                        }
                        else if (this.focusedComponent == null)
                        {
                            this.focusedComponent = value.Root;
                        }
                    }
                }
                else if (value == null)
                {
                    this.focusedComponent = null;
                }
            }
        }

        #endregion

        #region Control Data

        public MMControlCollection Components { get; protected set; } = new MMControlCollection();

        public void Add(IMMControlComponentInternal component)
        {
            if (component != null)
            {
                if (!this.Components.Contains(component as IMMControlComponent))
                {
                    // Restore parent relationship before adding
                    component.Parent?.Remove(component);

                    // Add to the top level components
                    this.Components.Add(component as IMMControlComponent);
                    component.Manager = this;
                    component.Parent = null;

                    // Set newly added component to focus
                    if (this.focusedComponent == null)
                    {
                        component.HasFocus = true;
                    }
                }
            }
        }

        /// <summary>
        /// Remove existing parenthood to original state.
        /// </summary>
        public void Remove(IMMControlComponent component)
        {
            if (component != null)
            {
                // Blur before removal
                if (component.HasFocus)
                {
                    component.HasFocus = false;
                }

                // Remove from children list
                this.Components.Remove(component);
            }
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
            // TODO: Update control
            base.Update(time);
        }

        #endregion
    }
}
