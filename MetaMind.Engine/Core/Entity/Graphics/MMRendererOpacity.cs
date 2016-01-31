namespace MetaMind.Engine.Core.Entity.Graphics
{
    using System;

    public class MMRendererOpacity : IMMRendererOpacity, IMMRendererOpacityInternal
    {
        public MMRendererOpacity(IMMRendererComponent target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            this.Target = target;

            this.CascadeEnabled = false;
        }

        #region Target Data

        public IMMRendererComponent Target { get; private set; }

        #endregion

        #region Opacity Data

        private byte blend = byte.MaxValue;

        /// <summary>
        /// Opacity influenced by parent's opacity.
        /// </summary>
        byte IMMRendererOpacity.Blend => this.blend;

        /// <summary>
        /// Opacity influenced by parent's opacity.
        /// </summary>
        byte IMMRendererOpacityInternal.Blend
        {
            get { return this.blend; }

            // Setter is only visible to the internal interface
            set { this.blend = value; }
        }

        private byte raw = byte.MaxValue;

        private bool cascadeEnabled;

        /// <summary>
        /// Opacity independent with parent's opacity. 
        /// </summary>
        public virtual byte Raw
        {
            get { return this.raw; }
            set
            {
                this.Blend = this.raw = value;

                if (this.CascadeEnabled)
                {
                    this.UpdateCascade(this.Target.Parent.Opacity);
                }
                else
                {
                    // TODO
                }
            }
        }

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
                        this.UpdateCascade(this.Target.Parent.Opacity);
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

        public virtual void UpdateDisplayed(IMMRendererOpacity parent)
        {
            this.UpdateDisplayedInItself(parent);
            this.UpdateDisplayedInChildren();
        }

        private void UpdateDisplayedInChildren()
        {
            if (this.IsCascaded)
            {
                foreach (var child in this.Target.Children)
                {
                    ((IMMRenderOpacityInternal)child.Opacity).UpdateDisplayed(this);
                }
            }
        }

        private void UpdateDisplayedInItself(IMMRendererOpacity parent)
        {
            var baseOpacity = parent?.Blend ?? 255;

            this.Blend = (byte)(this.Raw * baseOpacity / 255.0f);
        }

        #endregion

        #region Cascade Operations

        public virtual void DisableCascade()
        {
            this.Blend = this.Raw;

            foreach (var child in this.Target.Children)
            {
                ((IMMRenderOpacityInternal)child.Opacity).
                    UpdateDisplayed(null);
            }
        }

        public virtual void UpdateCascade(IMMRendererOpacity parent)
        {
            this.UpdateDisplayed(parent);
        }

        #endregion
    }
}