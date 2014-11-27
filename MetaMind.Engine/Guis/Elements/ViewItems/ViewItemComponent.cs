namespace MetaMind.Engine.Guis.Elements.ViewItems
{
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.Views;

    public interface IViewItemComponent
    {
        #region Item Components

        IViewItem Item { get; }

        dynamic ItemData { get; }

        dynamic ItemControl { get; }

        IItemGraphics ItemGraphics { get; }

        dynamic ItemSettings { get; }

        #endregion Item Components

        #region View Components

        IView View { get; }

        IViewControl ViewControl { get; }

        dynamic ViewSettings { get; }

        #endregion View Components
    }

    public class ViewItemComponent : EngineObject, IViewItemComponent
    {
        public ViewItemComponent(IViewItem item)
        {
            this.item = item;
        }

        #region Item Components

        private IViewItem item;

        public IViewItem Item { get { return this.item; } }

        public dynamic ItemData
        {
            get { return this.item.ItemData; }
        }

        public dynamic ItemControl
        {
            get { return this.item.ItemControl; }
        }

        public IItemGraphics ItemGraphics
        {
            get { return this.item.ItemGraphics; }
        }

        public dynamic ItemSettings
        {
            get { return this.item.ItemSettings; }
        }

        #endregion Item Components

        #region View Components

        public IView View
        {
            get { return this.item.View; }
        }

        public IViewControl ViewControl
        {
            get { return this.View.Control; }
        }

        public dynamic ViewSettings
        {
            get { return this.item.ViewSettings; }
        }

        #endregion View Components
    }
}