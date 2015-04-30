namespace MetaMind.Engine.Guis.Widgets.Items
{
    using MetaMind.Engine.Guis.Widgets.Views;

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

        public dynamic ItemLogic
        {
            get { return this.Item.ItemLogic; }
        }

        public dynamic ItemData
        {
            get { return this.Item.ItemData; }
        }

        public dynamic ItemSettings
        {
            get { return this.Item.ItemSettings; }
        }

        public IItemVisual ItemVisual
        {
            get { return this.Item.ItemVisual; }
        }

        public IView View
        {
            get { return this.Item.View; }
        }

        public dynamic ViewLogic
        {
            get { return this.View.ViewLogic; }
        }

        public dynamic ViewSettings
        {
            get { return this.Item.ViewSettings; }
        }

        #endregion View Components
    }
}