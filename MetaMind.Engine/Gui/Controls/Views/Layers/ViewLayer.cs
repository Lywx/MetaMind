namespace MetaMind.Engine.Gui.Controls.Views.Layers
{
    using System;
    using Item.Data;
    using Logic;
    using Scrolls;
    using Selections;
    using Settings;
    using Swaps;

    /// <summary>
    /// A extension layer for extended and customized implementation of specific view components.
    /// </summary>
    public class ViewLayer : ViewComponent, IViewLayer
    {
        protected ViewLayer(IMMViewNode view)
            : base(view)
        {
        }

        #region Indirect Dependency

        public IViewLogic ViewLogic => this.View.ViewLogic;

        public ViewSettings ViewSettings => this.View.ViewSettings;

        public IViewSelectionController ViewSelection => this.ViewLogic.ViewSelection;

        public IViewScrollController ViewScroll => this.ViewLogic.ViewScroll;

        public IViewSwapController ViewSwap => this.ViewLogic.ViewSwap;

        public IViewBinding ViewBinding => this.ViewLogic.ViewBinding;

        #endregion

        public T Get<T>() where T : class, IViewLayer
        {
            var type = this.GetType();
            if (type == typeof (T) ||
                type.IsSubclassOf(typeof (T)))
            {
                return this as T;
            }

            throw new InvalidOperationException(
                $"This is not a {typeof(T).Name}.");
        }
    }
}