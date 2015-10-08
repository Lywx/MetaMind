namespace MetaMind.Engine.Nodes
{
    using System;
    using Gui.Renders;

    public class MMNodeRenderer : MMRenderComponent, IMMNodeRenderer
    {
        public MMNodeRenderer(IMMNode target)
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
            get { return this.zOrder; }
            set
            {
                if (this.zOrder != value)
                {
                    if (this.Parent != null)
                        this.Parent.ReorderChild(this, value);

                    this.zOrder = value;

                    if (EventDispatcher != null)
                        EventDispatcher.MarkDirty = this;
                }
            }
        }

    }
}