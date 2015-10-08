namespace MetaMind.Engine.Nodes
{
    using System;
    using Microsoft.Xna.Framework;

    public class MMNodeColor : IMMNodeColor, IMMNodeColorInternal
    {
        #region Constructors and Finalizer

        public MMNodeColor(IMMNode target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            this.Target = target;
        }

        #endregion

        #region Events

        public event Action UpdateDisplayedInItselfStarted;

        public event Action UpdateDisplayedInItselfEnded;

        #endregion

        #region Target Data

        public IMMNode Target { get; private set; }

        #endregion

        #region Color Data

        private Color displayed = Color.White;

        public Color Displayed
        {
            get { return this.displayed; }
            set { this.displayed = value; }
        }

        public Color Real { get; set; } = Color.White;

        public virtual Color Standalone
        {
            get { return this.Real; }
            set
            {
                this.Displayed = this.Real = value;

                this.UpdateCascade(this.Target.Parent.Color);
            }
        }

        private bool isCascaded;

        /// <summary>
        /// Whether or not parent color would cascade into displayed color in 
        /// this color's children.
        /// </summary>
        /// <remarks>
        /// Default value is false. This property will not influence whether or 
        /// not this displayed color will be influenced by its parent.
        /// </remarks>
        public bool IsCascaded
        {
            get { return this.isCascaded; }
            set
            {
                var changed = this.isCascaded != value;
                if (changed)
                {
                    this.isCascaded = value;

                    if (this.isCascaded)
                    {
                        this.UpdateCascade(this.Target.Parent.Color);
                    }
                    else
                    {
                        this.DisableCascade();
                    }
                }
            }
        }

        #endregion

        #region Display Operations 

        public virtual void UpdateDisplayed(IMMNodeColor parent)
        {
            this.UpdateDisplayedInItselfStarted?.Invoke();
            this.UpdateDisplayedInItself(parent);
            this.UpdateDisplayedInItselfEnded?.Invoke();

            this.UpdateDisplayedInChildren();
        }

        private void UpdateDisplayedInChildren()
        {
            if (this.IsCascaded)
            {
                foreach (var child in this.Target.Children)
                {
                    ((IMMNodeColorInternal)child.Color).UpdateDisplayed(this);
                }
            }
        }

        private void UpdateDisplayedInItself(IMMNodeColor parent)
        {
            var baseColor = parent?.Displayed ?? Color.White;

            this.displayed.R = (byte)(this.Real.R * baseColor.R / 255.0f);
            this.displayed.G = (byte)(this.Real.G * baseColor.G / 255.0f);
            this.displayed.B = (byte)(this.Real.B * baseColor.B / 255.0f);
        }

        #endregion

        #region Cascade Operations

        public void UpdateCascade(IMMNodeColor parent)
        {
            this.UpdateDisplayed(parent);
        }

        public void DisableCascade()
        {
            foreach (var child in this.Target.Children)
            {
                ((IMMNodeColorInternal)child.Color).UpdateDisplayed(null);
            }
        }

        #endregion
    }
}