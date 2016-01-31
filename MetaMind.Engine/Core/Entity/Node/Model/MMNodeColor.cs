namespace MetaMind.Engine.Core.Entity.Node.Model
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

        #region Target Data

        public IMMNode Target { get; private set; }

        #endregion

        #region Color Data

        public IMMNodeColor Parent => this.Target.Parent?.Color;

        private Color blend = Color.White;

        public Color Blend
        {
            get { return this.blend; }

            // Setter is only visible through the internal interface
            set { this.blend = value; }
        }

        private Color raw = Color.White;

        public virtual Color Raw
        {
            get { return this.raw; }
            set
            {
                this.Blend = this.raw = value;

                // Process the blend color after the assignment
                this.UpdateCascade(this.Target.Parent.Color);
            }
        }

        private bool cascadeEnabled;

        /// <summary>
        /// Whether or not parent color would cascade into displayed color in 
        /// this color's children.
        /// </summary>
        /// <remarks>
        /// Default value is false. This property will not influence whether or 
        /// not this displayed color will be influenced by its parent.
        /// </remarks>
        public bool CascadeEnabled
        {
            get { return this.cascadeEnabled; }
            set
            {
                var changed = this.cascadeEnabled != value;
                if (changed)
                {
                    this.cascadeEnabled = value;

                    if (this.cascadeEnabled)
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

        /// <summary>
        /// Update blend color cascadedly from parent to children.
        /// </summary>
        public virtual void UpdateBlend()
        {
            this.UpdateBlend();
            this.UpdateBlendToChildren();
        }

        private void UpdateBlendToChildren()
        {
            if (this.CascadeEnabled)
            {
                foreach (var child in this.Target.Children)
                {
                    ((IMMNodeColorInternal)child.Color).UpdateBlend();
                }
            }
        }

        private void UpdateBlend(Color @base)
        {
            this.blend.R = (byte)(this.raw.R * @base.R / 255.0f);
            this.blend.G = (byte)(this.raw.G * @base.G / 255.0f);
            this.blend.B = (byte)(this.raw.B * @base.B / 255.0f);
        }

        #endregion

        #region Cascade Operations

        public void UpdateCascade()
        {
            this.UpdateBlend();
        }

        public void DisableCascade()
        {
            foreach (var child in this.Target.Children)
            {
                ((IMMNodeColorInternal)child.Color).UpdateBlend(Color.White);
            }
        }

        #endregion
    }
}