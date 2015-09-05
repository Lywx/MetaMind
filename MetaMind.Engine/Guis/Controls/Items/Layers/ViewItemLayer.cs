namespace MetaMind.Engine.Guis.Controls.Items.Layers
{
    using System;
    using Logic;
    using Settings;

    /// <summary>
    /// A extension layer for extended and customized implementation of specific item components.
    /// </summary>
    public class ViewItemLayer : ViewItemComponent, IViewItemLayer
    {
        protected ViewItemLayer(IViewItem item)
            : base(item)
        {
        }

        #region Layer

        /// <summary>
        /// Gets a interface to most general item logic.
        /// </summary>
        public IViewItemLogic ItemLogic => this.Item.ItemLogic;

        /// <summary>
        /// Gets a interface to most general data type.
        /// </summary>
        public dynamic ItemData => this.Item.ItemData;

        /// <summary>
        /// Gets a interface to most general item settings.
        /// </summary>
        public ItemSettings ItemSettings => this.Item.ItemSettings;

        #endregion

        #region Layer Operations

        public T Get<T>() where T : class, IViewItemLayer
        {
            var type = this.GetType();
            if (type == typeof (T) ||
                type.IsSubclassOf(typeof (T)))
            {
                return this as T;
            }

            throw new InvalidOperationException($"This is not a {typeof(T).Name}.");
        }

        #endregion
    }
}