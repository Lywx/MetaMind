using System;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Settings;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IViewComponent
    {
        dynamic Control { get; }
        IViewGraphics Graphics { get; }
        IView View { get; }
        dynamic ViewSettings { get; }
        dynamic ItemSettings { get; }
    }

    public class ViewComponent : EngineObject, IViewComponent
    {
        private readonly IView view;
        private readonly dynamic viewSettings;
        private readonly dynamic itemSettings;

        public dynamic Control { get { return view.Control; } }
        public IViewGraphics Graphics { get { return view.Graphics; } }

        public IView View { get { return view; } }
        public dynamic ViewSettings { get { return viewSettings; } }
        public dynamic ItemSettings { get { return itemSettings; } }

        protected ViewComponent( IView view, ICloneable viewSettings, ICloneable itemSettings )
        {
            this.view = view;
            this.viewSettings = viewSettings;
            this.itemSettings = itemSettings;
        }
    }
}