namespace MetaMind.Engine.Entities.Nodes
{
    using System;
    using Graphics;

    public class MMNodeRenderer : MMRendererComponent, IMMNodeRenderer
    {
        public MMNodeRenderer(IMMNode target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            this.Color = new MMNodeColor(target);
            this.Target = target;
        }

        public IMMNode Target { get; set; }

        public IMMNodeColor Color { get; set; }
    }
}