namespace MetaMind.Engine.Entities.Graphics
{
    using System;

    public class MMRendererOpacity : IMMRendererOpacity, IMMRenderOpacityInternal 
    {
        public MMRendererOpacity(IMMRendererComponent target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            this.Target = target;

            this.IsCascaded = false;
        }

        #region Events

        public event Action UpdateDisplayedInItselfStarted = delegate {};

        public event Action UpdateDisplayedInItselfEnded = delegate {};

        #endregion

        #region Target Data

        public IMMRendererComponent Target { get; private set; }

        #endregion

        #region Opacity Data

        /// <summary>
        /// Opacity independent with parent's opacity. 
        /// </summary>
        public byte Real { get; set; } = byte.MaxValue;

        /// <summary>
        /// Opacity influenced by parent's opacity.
        /// </summary>
        public byte Displayed { get; set; } = byte.MaxValue;

        /// <summary>
        /// Opacity independent with parent's opacity. 
        /// </summary>
        public virtual byte Standard
        {
            get { return this.Real; }
            set
            {
                this.Displayed = this.Real = value;

                this.UpdateCascade(this.Target.Parent.Opacity);
            }
        }

        private bool isCascaded;

        public bool IsCascaded
        {
            get { return this.isCascaded; }
            set
            {
                if (this.isCascaded == value)
                {
                    return;
                }

                this.isCascaded = value;

                if (this.isCascaded)
                {
                    this.UpdateCascade(this.Target.Parent.Opacity);
                }
                else
                {
                    this.DisableCascade();
                }
            }
        }

        #endregion

        #region Display Operations

        public virtual void UpdateDisplayed(IMMRendererOpacity parent)
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
                    ((IMMRenderOpacityInternal)child.Opacity).
                        UpdateDisplayed(this);
                }
            }
        }

        private void UpdateDisplayedInItself(IMMRendererOpacity parent)
        {
            var baseOpacity = (parent?.Displayed ?? 255);

            this.Displayed = (byte)(this.Standard * baseOpacity / 255.0f);
        }

        #endregion

        #region Cascade Operations

        public virtual void DisableCascade()
        {
            this.Displayed = this.Standard;

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