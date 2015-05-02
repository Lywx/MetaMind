namespace MetaMind.Engine.Guis.Widgets.Items
{
    using MetaMind.Engine.Guis.Widgets.Items.Extensions;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.Extensions;

    public class ViewItemComponent : GameControllableEntity, IViewItemComponent
    {
        #region Constructors and Destructors

        protected ViewItemComponent(IViewItem item)
        {
            this.Item = item;
        }

        ~ViewItemComponent()
        {
            this.Dispose();
        }

        #endregion Constructors and Destructors

        #region Direct Dependency

        public IViewItem Item { get; private set; }

        #endregion 

        #region Indirect Dependency

        public IView View
        {
            get
            {
                return this.Item.View;
            } 
        }

        public IViewExtension ViewExtension
        {
            get
            {
                return this.View.ViewExtension;
            }
        }

        public IViewItemExtension ItemExtension
        {
            get
            {
                return this.Item.ItemExtension;
            }
        }

        #endregion 
    }
}