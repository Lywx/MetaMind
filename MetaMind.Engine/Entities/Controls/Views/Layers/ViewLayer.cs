namespace MetaMind.Engine.Entities.Controls.Views.Layers
{
    using System;
    using Controllers;
    using Item.Data;
    using Scrolls;
    using Selections;
    using Settings;
    using Swaps;

    /// <summary>
    /// A extension layer for extended and customized implementation of specific view components.
    /// </summary>
    public class ViewLayer : MMViewControlComponent, IMMViewLayer
    {
        protected ViewLayer(IMMView view)
            : base(view)
        {
        }

        #region Indirect Dependency

        public IMMViewController ViewController => this.View.ViewController;

        public ViewSettings ViewSettings => this.View.ViewSettings;

        public IMMViewSelectionController ViewSelection => this.ViewController.ViewSelection;

        public IMMViewScrollController ViewScroll => this.ViewController.ViewScroll;

        public IMMViewSwapController ViewSwap => this.ViewController.ViewSwap;

        public IMMViewBinding ViewBinding => this.ViewController.ViewBinding;

        #endregion

        public T Get<T>() where T : class, IMMViewLayer
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