namespace MetaMind.Engine.Node
{
    using System;
    using Gui.Renders;

    public class MMRenderOpacity : IMMRenderOpacity, IMMRenderOpacityInternal 
    {
        public MMRenderOpacity(IMMRenderComponent target)
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

        public IMMRenderComponent Target { get; private set; }

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
        public virtual byte Standalone
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

        public virtual void UpdateDisplayed(IMMRenderOpacity parent)
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

        private void UpdateDisplayedInItself(IMMRenderOpacity parent)
        {
            var baseOpacity = (parent?.Displayed ?? 255);

            this.Displayed = (byte)(this.Standalone * baseOpacity / 255.0f);
        }

        #endregion

        #region Cascade Operations

        public virtual void DisableCascade()
        {
            this.Displayed = this.Standalone;

            foreach (var child in this.Target.Children)
            {
                ((IMMRenderOpacityInternal)child.Opacity).
                    UpdateDisplayed(null);
            }
        }

        public virtual void UpdateCascade(IMMRenderOpacity parent)
        {
            this.UpdateDisplayed(parent);
        }

        #endregion
    }
}