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

        public List<IViewItem> Item
        {
            get
            {
                return this.View.Items;
            }
        }

        #endregion

        #region Layering

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

        public virtual void SetupLayer() { }

        #endregion

        #region Component Tree

        public T ViewGetComponent<T>(string id) where T : class
        {
            var t = (T)this.ViewGetComponent(id);
            if (t == null)
            {
                throw new InvalidOperationException(string.Format("ViewComponents has no child {0} of type {1}", id, typeof(T).Name));
            }

            return t;
        }

        private object ViewGetComponent(string id)
        {
            return this.View.ViewComponents.ContainsKey(id) ? this.View.ViewComponents[id] : null;
        }

        #endregion
    }
}