namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Guis.Widgets.Regions;

    using Microsoft.Xna.Framework;

    public delegate Rectangle ViewRegionPositioning(dynamic viewSettings, dynamic itemSettings);

    public class PointViewRegion : Region, IViewComponent
    {
        public PointViewRegion(IView view, PointViewSettings1D viewSettings, ICloneable itemSettings, ViewRegionPositioning positioning)
            : base(positioning(viewSettings, itemSettings))
        {
            this.View         = view;
            this.ViewSettings = viewSettings;
            this.ItemSettings = itemSettings;

            this.Positioning = positioning;
        }

        public PointViewRegion(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings, ViewRegionPositioning positioning)
            : base(positioning(viewSettings, itemSettings))
        {
            this.View         = view;
            this.ViewSettings = viewSettings;
            this.ItemSettings = itemSettings;

            this.Positioning = positioning;
        }

        public dynamic ItemSettings { get; private set; }

        public IView View { get; private set; }

        public dynamic ViewControl
        {
            get { return this.View.Control; }
        }

        public dynamic ViewSettings { get; private set; }

        private ViewRegionPositioning Positioning { get; set; }

        public void Clear()
        {
            this.Frame[FrameState.Mouse_Left_Clicked] = () => false;
            this.Frame[FrameState.Mouse_Left_Double_Clicked] = () => false;
            this.Frame[FrameState.Mouse_Right_Clicked] = () => false;
            this.Frame[FrameState.Mouse_Right_Double_Clicked] = () => false;
        }

        public override void Update(GameTime time)
        {
            this.UpdateRegionGeometry();
        }

        private void UpdateRegionGeometry()
        {
            this.Location = this.Positioning(this.ViewSettings, this.ItemSettings).Location;
        }
    }
}