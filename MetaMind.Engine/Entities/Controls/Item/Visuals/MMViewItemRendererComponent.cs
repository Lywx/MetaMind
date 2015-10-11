namespace MetaMind.Engine.Entities.Controls.Item.Visuals
{
    using Layers;
    using Microsoft.Xna.Framework;
    using Nodes;
    using Views.Layers;

    public abstract class MMViewItemRendererComponent : MMNodeRenderer, IMMViewItemRendererComponent
    {
        public MMViewItemRendererComponent(IMMViewItem item)
            : base(item)
        {
        }

        /// <remarks>
        /// Forced reimplementation.
        /// </remarks>>
        public abstract override void Draw(GameTime time);

        public T GetViewLayer<T>() where T : class, IMMViewLayer
        {
            throw new System.NotImplementedException();
        }

        public T GetItemLayer<T>() where T : class, IMMViewItemLayer
        {
            throw new System.NotImplementedException();
        }
    }
}