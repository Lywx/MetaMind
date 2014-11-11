using System;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IViewComponent
    {
        dynamic ViewControl  { get; }
        IView   View         { get; }
        dynamic ViewSettings { get; }
        dynamic ItemSettings { get; }
    }

    public class ViewComponent : EngineObject, IViewComponent
    {
        public dynamic ViewControl { get { return View.Control; } }

        public IView View { get; private set; }
        public dynamic ViewSettings { get; private set; }
        public dynamic ItemSettings { get; private set; }

        protected ViewComponent( IView view, ICloneable viewSettings, ICloneable itemSettings )
        {
            View         = view;
            ViewSettings = viewSettings;
            ItemSettings = itemSettings;
        }
    }
}