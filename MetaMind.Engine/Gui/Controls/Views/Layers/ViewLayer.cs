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
    public class ViewLayer : MMViewControlComponent, IViewLayer
    {
        protected ViewLayer(IMMViewNode view)
            : base(view)
        {
        }

        #region Indirect Dependency

        public IMMViewController ViewController => this.View.ViewController;

        public ViewSettings ViewSettings => this.View.ViewSettings;

        public IViewSelectionController ViewSelection => this.ViewController.ViewSelection;

        public IViewScrollController ViewScroll => this.ViewController.ViewScroll;

        public IViewSwapController ViewSwap => this.ViewController.ViewSwap;

        public IViewBinding ViewBinding => this.ViewController.ViewBinding;

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