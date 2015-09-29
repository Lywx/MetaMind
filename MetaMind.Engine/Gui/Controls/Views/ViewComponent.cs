// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewComponent.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Views
{
    using System;
    using System.Collections.Generic;
    using Components;
    using Item;
    using Item.Layers;
    using Layers;

    /// <summary>
    ///     ViewComponent hooks all necessary external information to the View object,
    ///     which allows view-wise substitution of settings. The dynamic typing allows
    ///     customization
    /// </summary>
    public abstract class ViewComponent : Component, IViewComponent
    {
        #region Constructors

        protected ViewComponent(IView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            this.View = view;
        }

        #endregion

        #region Direct Dependency

        public IView View { get; private set; }

        #endregion

        #region Indirect Dependency

        public List<IViewItem> ItemsRead => this.View.ItemsRead;

        public List<IViewItem> ItemsWrite => this.View.ItemsWrite;

        #endregion

        #region Layer

        private IViewLayer ViewLayer => this.View.ViewLayer;

        public T GetViewLayer<T>() where T : class, IViewLayer
        {
            return this.ViewLayer.Get<T>();
        }

        public T GetItemLayer<T>(IViewItem item) where T : class, IViewItemLayer
        {
            return item.GetLayer<T>();
        }

        #endregion

        #region Component Tree

        public Dictionary<string, object> ViewComponents => this.View.ViewComponents;

        public T GetComponent<T>(string id) where T : class
        {
            return this.View.GetComponent<T>(id);
        }

        #endregion
    }
}