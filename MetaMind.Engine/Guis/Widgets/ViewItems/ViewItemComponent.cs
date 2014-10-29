using System;
using MetaMind.Engine.Guis.Groups;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.Views;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public interface IViewItemComponent
    {
        #region Item Components

        IViewItem Item { get; }
        IViewItemData ItemData { get; }
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
        public ViewItemComponent( IViewItem item )
        {
            this.item = item;
        }

        #region Item Components

        private IViewItem item;
        public IViewItem Item { get { return item; } }

        public IViewItemData ItemData
        {
            get { return item.ItemData; }
            
        }

        public dynamic ItemControl
        {
            get { return item.ItemControl; }
        }

        public IItemGraphics ItemGraphics
        {
            get { return item.ItemGraphics; }
        }
        public dynamic ItemSettings
        {
            get { return item.ItemSettings; }
        }

        #endregion Item Components

        #region View Components

        public IView View
        {
            get { return item.View; }
        }

        public IViewControl ViewControl
        {
            get { return View.Control; }
        }
        public dynamic ViewSettings
        {
            get { return item.ViewSettings; }
        }

        #endregion View Components
    }
}