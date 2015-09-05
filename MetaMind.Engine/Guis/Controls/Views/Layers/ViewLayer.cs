namespace MetaMind.Engine.Guis.Widgets.Views.Layers
{
    using System;
    using Items.Data;
    using Logic;
    using Settings;
    using Scrolls;
    using Selections;
    using Swaps;

    /// <summary>
    /// A extension layer for extended and customized implementation of specific view components.
    /// </summary>
    public class ViewLayer : ViewComponent, IViewLayer
    {
        protected ViewLayer(IView view)
            : base(view)
        {
        }

        #region Indirect Depedency

        public IViewLogic ViewLogic
        {
            get
            {
                return this.View.ViewLogic;
            }
        }

        public ViewSettings ViewSettings
        {
            get { return this.View.ViewSettings; }
        }

        public IViewSelectionController ViewSelection
        {
            get { return this.ViewLogic.ViewSelection; }
        }

        public IViewScrollController ViewScroll
        {
            get { return this.ViewLogic.ViewScroll; }
        }

        public IViewSwapController ViewSwap
        {
            get { return this.ViewLogic.ViewSwap; }
        }

        public IViewBinding ViewBinding
        {
            get { return this.ViewLogic.ViewBinding; }
        }

        #endregion

        public T Get<T>() where T : class, IViewLayer
        {
            var type = this.GetType();
            if (type == typeof (T) ||
                type.IsSubclassOf(typeof (T)))
            {
                return this as T;
            }

            throw new InvalidOperationException(string.Format("This is not a {0}.", typeof(T).Name));
        }
    }
}