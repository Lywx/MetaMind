﻿namespace MetaMind.EngineTest.Guis
{
    using Engine;
    using Engine.Core.Entity;
    using Engine.Core.Entity.Common;
    using Engine.Core.Entity.Control.Regions;
    using Microsoft.Xna.Framework;

    public class PlayTest_Region : MMMVCEntity<object>
    {
        private MMEntityCollection<MMInputtableEntity> control;
        private MMEntityCollection<MMVisualEntity> visual;
        private RectangleRegion region;

        public PlayTest_Region(object settings)
            : base(settings)
        {
            this.control = new MMEntityCollection<MMInputtableEntity>();
            this.visual  = new MMEntityCollection<MMVisualEntity>();

            // Region Control
            this.region = new RectangleRegion(new Rectangle(50, 50, 50, 50));
            this.control.Add(region);

            // Box Visual
            var box = new ImageBox(() => region.ImmRectangle, () => Color.CornflowerBlue, () => true);
            this.visual.Add(box);
        }

        public override void Draw(GameTime time)
        {
            this.control.Draw(time);
            this.visual .Draw(time);

            StateVisualTester.Draw(graphics, typeof(RegionState), this.region.RegionStates, this.region.Location.ToVector2(), 10, 10);
        }

        public override void Update(GameTime time)
        {
            this.visual .Update(time);
            this.control.Update(time);
        }

        public override void UpdateInput(GameTime time)
        {
            this.control.UpdateInput(time);
        }
    }
}