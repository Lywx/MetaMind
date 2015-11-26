// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewComponent.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Entities.Controls.Views
{
    using System;
    using System.Collections.Generic;
    using Item;
    using Item.Layers;
    using Layers;

    /// <summary>
    ///     ViewComponent hooks all necessary external information to the View object,
    ///     which allows view-wise substitution of settings. The dynamic typing allows
    ///     customization
    /// </summary>
    public abstract class MMViewControlComponent : MMControlComponent, IMMViewComponent
    {
        #region Constructors

        protected MMViewControlComponent(IMMView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            this.View = view;
        }

        #endregion

        #region Direct Dependency

        public IMMView View { get; private set; }

        #endregion

        #region Indirect Dependency

        public List<IMMViewItem> Items => this.View.Items;

        internal List<IMMViewItem> ItemsWrite
        {
            get { return ((IMMViewInternal)this.View).ItemsWrite; }
            set { ((IMMViewInternal)this.View).ItemsWrite = value; }
        }

        #endregion

        #region Layer

        private IMMViewLayer ViewLayer => this.View.ViewLayer;

        public T GetViewLayer<T>() where T : class, IMMViewLayer
        {
            return this.ViewLayer.Get<T>();
        }

        public T GetItemLayer<T>(IMMViewItem item) where T : class, IMMViewItemLayer
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