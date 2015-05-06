namespace MetaMind.Engine.Guis.Widgets.Views.Layers
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;

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

        #endregion


        public T Get<T>()
        {
            if (this.GetType().IsSubclassOf(typeof(T)))
            {
                return (T)Convert.ChangeType(this, typeof(T));
            }

            throw new InvalidOperationException(string.Format("This is not a {0}.", typeof(T).Name));
        }
    }
}