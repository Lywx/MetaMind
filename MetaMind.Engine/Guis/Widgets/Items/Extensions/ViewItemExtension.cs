namespace MetaMind.Engine.Guis.Widgets.Items.Extensions
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items.Logic;
    using MetaMind.Engine.Guis.Widgets.Items.Settings;

    /// <summary>
    /// A extension layer for extended and customized implementation of specific item components.
    /// </summary>
    public class ViewItemExtension : ViewItemComponent, IViewItemExtension
    {
        protected ViewItemExtension(IViewItem item)
            : base(item)
        {
        }

        public IViewItemLogic ItemLogic
        {
            get { return this.Item.ItemLogic; }
        }

        public dynamic ItemData
        {
            get { return this.Item.ItemData; }
        }

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