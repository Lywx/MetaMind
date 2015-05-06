namespace MetaMind.Engine.Guis.Widgets.Items.Layers
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items.Logic;
    using MetaMind.Engine.Guis.Widgets.Items.Settings;

    /// <summary>
    /// A extension layer for extended and customized implementation of specific item components.
    /// </summary>
    public class ViewItemLayer : ViewItemComponent, IViewItemLayer
    {
        protected ViewItemLayer(IViewItem item)
            : base(item)
        {
        }

        /// <summary>
        /// Gets a interface to most general item logic.
        /// </summary>
        public IViewItemLogic ItemLogic
        {
            get { return this.Item.ItemLogic; }
        }

        /// <summary>
        /// Gets a interface to most general data type.
        /// </summary>
        public dynamic ItemData
        {
            get { return this.Item.ItemData; }
        }

        /// <summary>
        /// Gets a interface to most general item settings.
        /// </summary>
        public ItemSettings ItemSettings
        {
            get { return this.Item.ItemSettings; }
        }

        public T Get<T>() where T : class
        {
            if (this.GetType().IsSubclassOf(typeof(T)))
            {
                return (T)Convert.ChangeType(this, typeof(T));
            }

            throw new InvalidOperationException(string.Format("This is not a {0}.", typeof(T).Name));
        }
    }
}