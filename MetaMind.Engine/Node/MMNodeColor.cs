// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Node
{
    using Microsoft.Xna.Framework;

    public class MMNodeColor
    {
        #region Color Data

        public virtual Color Color
        {
            get { return this.RealColor; }
            set
            {
                this.displayedColor = this.RealColor = value;

                this.UpdateCascadeColor();
            }
        }

        private Color displayedColor;

        public Color DisplayedColor
        {
            get { return this.displayedColor; }
            protected set
            {
                this.displayedColor = value;
            }
        }

        protected Color RealColor { get; set; }

        private bool isColorCascaded;

        public virtual bool IsColorCascaded
        {
            get { return this.isColorCascaded; }
            set
            {
                if (this.isColorCascaded == value)
                {
                    return;
                }

                this.isColorCascaded = value;

                if (this.isColorCascaded)
                {
                    this.UpdateCascadeColor();
                }
                else
                {
                    this.DisableCascadeColor();
                }
            }
        }

        #endregion

        public MMNodeColor()
        {
            this.IsColorCascaded = false;

            this.displayedColor  = Color.White;
            this.RealColor       = Color.White;
        }

        #region Operations 

        public virtual void UpdateColor()
        {
            // Override the update of color here
        }

        public virtual void UpdateDisplayedColor(Color parentColor)
        {
            this.displayedColor.R = (byte)(this.RealColor.R * parentColor.R / 255.0f);
            this.displayedColor.G = (byte)(this.RealColor.G * parentColor.G / 255.0f);
            this.displayedColor.B = (byte)(this.RealColor.B * parentColor.B / 255.0f);

            this.UpdateColor();

            if (this.IsColorCascaded)
            {
                if (IsOpacityCascaded && Children != null)
                {
                    foreach (MMNode node in Children)
                    {
                        if (node != null)
                        {
                            node.UpdateDisplayedColor(this.DisplayedColor);
                        }
                    }
                }
            }
        }

        protected internal void UpdateCascadeColor(MMNodeColor parent)
        {
            var parentColor = Color.White;
            if (parent != null && parent.IsColorCascaded)
            {
                parentColor = parent.DisplayedColor;
            }

            this.UpdateDisplayedColor(parentColor);
        }

        protected internal void DisableCascadeColor()
        {
            if (Children == null)
                return;

            foreach (var child in Children)
            {
                child.UpdateDisplayedColor(Color.White);
            }
        }

        #endregion
    }
}