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

        #region Item Components

        public IViewItem Item { get; private set; }

        public dynamic ItemControl
        {
            get { return this.Item.ItemLogic; }
        }

        public dynamic ItemData
        {
            get { return this.Item.ItemData; }
        }

        public IItemVisualControl ItemVisualControl
        {
            get { return this.Item.ItemVisual; }
        }

        public dynamic ItemSettings
        {
            get { return this.Item.ItemSettings; }
        }

        #endregion Item Components

        #region View Components

        public IView View
        {
            get { return this.Item.View; }
        }

        public dynamic ViewControl
        {
            get { return this.View.Logic; }
        }

        public dynamic ViewSettings
        {
            get { return this.Item.ViewSettings; }
        }

        #endregion View Components
    }
}