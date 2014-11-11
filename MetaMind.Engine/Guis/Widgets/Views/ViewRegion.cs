using MetaMind.Engine.Guis.Elements.Frames;
using MetaMind.Engine.Guis.Elements.Regions;
using Microsoft.Xna.Framework;
using System;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public delegate Rectangle ViewRegionPositioning(dynamic viewSettings, dynamic itemSettings);

    public class ViewRegion : Region, IViewComponent
    {
        public ViewRegion(IView view, ViewSettings1D viewSettings, ICloneable itemSettings, ViewRegionPositioning positioning)
            : base(positioning(viewSettings, itemSettings))
        {
            View         = view;
            ViewSettings = viewSettings;
            ItemSettings = itemSettings;

            Positioning = positioning;
        }

        public ViewRegion(IView view, ViewSettings2D viewSettings, ICloneable itemSettings, ViewRegionPositioning positioning)
            : base(positioning(viewSettings, itemSettings))
        {
            View         = view;
            ViewSettings = viewSettings;
            ItemSettings = itemSettings;

            Positioning = positioning;
        }

        public ViewRegionPositioning Positioning  { get; private set; }

        public IView                 View         { get; private set; }

        public dynamic               ViewSettings { get; private set; }

        public dynamic               ItemSettings { get; private set; }

        public dynamic ViewControl
        {
            get { return View.Control; }
        }

        public override void UpdateStructure( GameTime gameTime )
        {
            UpdateRegionLogics();
            UpdateRegionGeometry();
        }

        private void UpdateRegionGeometry()
        {
            Location = Positioning(ViewSettings, ItemSettings).Location;
        }

        private void UpdateRegionLogics()
        {
            if (Frame.IsEnabled(FrameState .Frame_Active) && 
                this .IsEnabled(RegionState.Region_Hightlighted))
            {
                View.Enable(ViewState.View_Has_Focus);
            }
            else
            {
                View.Disable(ViewState.View_Has_Focus);
            }
        }
    }
}