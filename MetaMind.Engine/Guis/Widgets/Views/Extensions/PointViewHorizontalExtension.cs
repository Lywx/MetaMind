namespace MetaMind.Engine.Guis.Widgets.Views.Extensions
{
    using MetaMind.Engine.Guis.Widgets.Views.Settings;

    public class PointViewHorizontalExtension : ViewExtension
    {
        protected PointViewHorizontalExtension(IView view)
            : base(view)
        {
        }

        public new PointViewHorizontalSettings ViewSettings
        {
            get
            {
                return (PointViewHorizontalSettings)base.ViewSettings;
            }
        }
    }
}