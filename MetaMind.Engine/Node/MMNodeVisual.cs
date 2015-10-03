namespace MetaMind.Engine.Node
{
    using System;
    using Gui.Renders;

    public class MMNodeVisual : MMRenderComponent, IMMNodeVisual
    {
        public MMNodeVisual(IMMNode target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            this.Target = target;

            this.Color = new MMNodeColor(target);
        }

        public IMMNode Target { get; set; }

        public IMMNodeColor Color { get; set; }

        int zOrder;

        public int ZOrder
        {
            get { return zOrder; }
            set
            {
                if (zOrder != value)
                {
                    if (this.Parent != null)
                        this.Parent.ReorderChild(this, value);

                    zOrder = value;

                    if (EventDispatcher != null)
                        EventDispatcher.MarkDirty = this;
                }
            }
        }

    }
}