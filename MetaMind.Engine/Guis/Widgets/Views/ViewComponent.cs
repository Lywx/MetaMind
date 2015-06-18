// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewComponent.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;
    using System.Collections.Generic;
    using Items.Layers;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;

    /// <summary>
    ///     ViewComponent hooks all neccesary external information to the View object,
    ///     which allows view-wise substitution of settings. The dynamic typing allows
    ///     customization
    /// </summary>
    public abstract class ViewComponent : GameControllableEntity, IViewComponent
    {
        #region Constructors

        protected ViewComponent(IView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            this.View = view;
        }

        #endregion

        #region Direct Dependency

        public IView View { get; private set; }

        #endregion

        #region Indirect Dependency

        public List<IViewItem> ItemsRead
        {
            get
            {
                return this.View.ItemsRead;
            }
        }

        public List<IViewItem> ItemsWrite
        {
            get
            {
                return this.View.ItemsWrite; 
            }
        }

        #endregion

        #region Layer

        private IViewLayer ViewLayer
        {
            get
            {
                return this.View.ViewLayer;
            }
        }

        public T ViewGetLayer<T>() where T : class, IViewLayer
        {
            return this.ViewLayer.Get<T>();
        }

        public T ItemGetLayer<T>(IViewItem item) where T : class, IViewItemLayer
        {
            return item.GetLayer<T>();
        }

        public virtual void SetupLayer() { }

        #endregion

        #region Component Tree

        public Dictionary<string, object> ViewComponents
        {
            get { return this.View.ViewComponents; }
        }

        public T GetComponent<T>(string id) where T : class
        {
            return this.View.GetComponent<T>(id);
        }

        #endregion
    }
}