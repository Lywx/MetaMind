namespace MetaMind.Engine.Core.Entity.Graphics
{
    using System;

    public class MMRendererOpacity : IMMRendererOpacity
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

        /// <summary>
        /// Opacity blend with parent's opacity.
        /// </summary>
        public byte Blend { get;

            // Setter is only visible to the internal
            internal set; } = byte.MaxValue;

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

        public virtual void UpdateBlend(IMMRendererOpacity parent)
        {
            this.UpdateBlendFromParent(parent);
            this.UpdateBlendToChildren();
        }

        private void UpdateBlendToChildren()
        {
            if (this.IsCascaded)
            {
                foreach (var child in this.Target.Children)
                {
                    ((MMRendererOpacity)child.Opacity).UpdateBlend(this);
                }
            }
        }

        private void UpdateBlendFromParent(byte @base)
        {
            this.Blend = (byte)(this.Raw * @base / 255.0f);
        }

        #endregion

        #region Cascade Operations

        public virtual void DisableCascade()
        {
            this.Blend = this.Raw;

            foreach (var child in this.Target.Children)
            {
                ((MMRendererOpacity)child.Opacity).UpdateBlend(null);
            }
        }

        public virtual void UpdateCascade(IMMRendererOpacity parent)
        {
            this.UpdateBlend(parent);
        }

        #endregion
    }
}