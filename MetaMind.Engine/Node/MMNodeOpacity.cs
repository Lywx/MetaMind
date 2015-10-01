// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Node
{
    public class MMNodeOpacity
    {
        public MMNodeOpacity(MMNode target)
        {
            this.Target = target;

            this.IsOpacityCascaded = false;
        }

        public MMNode Target { get; set; }

        #region Opacity Data

        private byte displayedOpacity = byte.MaxValue;

        public byte DisplayedOpacity
        {
            get { return this.displayedOpacity; }
            protected set { this.displayedOpacity = value; }
        }

        public virtual byte Opacity
        {
            get { return this.RealOpacity; }
            set
            {
                this.displayedOpacity = this.RealOpacity = value;

                this.UpdateCascadeOpacity(this.Target.Parent.Graphics.);
            }
        }

        protected byte RealOpacity { get; set; } = byte.MaxValue;

        #endregion

        private bool isOpacityCascaded;

        public bool IsOpacityCascaded
        {
            get { return this.isOpacityCascaded; }
            set
            {
                if (this.isOpacityCascaded == value)
                {
                    return;
                }

                this.isOpacityCascaded = value;

                if (this.isOpacityCascaded)
                {
                    this.UpdateCascadeOpacity(this.Target.Parent.Color);
                }
                else
                {
                    this.DisableCascadeOpacity();
                }
            }
        }

        #region Opacity Operations

        protected virtual void DisableCascadeOpacity()
        {
            this.DisplayedOpacity = this.RealOpacity;

            foreach (MMNode node in Children.Elements)
            {
                node.UpdateDisplayedOpacity(255);
            }
        }

        protected internal virtual void UpdateDisplayedOpacity(byte parentOpacity)
        {
            this.displayedOpacity = (byte)(this.RealOpacity * parentOpacity / 255.0f);

            this.UpdateColor();

            if (this.IsOpacityCascaded && Children != null)
            {
                foreach (MMNode node in Children)
                {
                    node.UpdateDisplayedOpacity(this.DisplayedOpacity);
                }
            }
        }

        protected internal virtual void UpdateCascadeOpacity(MMNodeOpacity parent)
        {
            byte parentOpacity = 255;

            if (parent != null && parent.IsOpacityCascaded)
            {
                parentOpacity = parent.DisplayedOpacity;
            }

            this.UpdateDisplayedOpacity(parentOpacity);
        }

        #endregion
    }
}