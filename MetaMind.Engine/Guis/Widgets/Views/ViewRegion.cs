namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using Microsoft.Xna.Framework;

    using Regions;

    public class ViewRegion : Region
    {
        public ViewRegion(Func<Rectangle> bounds)
            : base(bounds())
        {
            this.Bounds = bounds;
        }

        #region Dependency

        protected Func<Rectangle> Bounds { get; set; }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            this.Rectangle = this.Bounds();
        }

        #endregion

        #region Operations

        public void Blur()
        {
            this.StateMachine.Fire(Trigger.PressedOutside);
        }

        #endregion
    }
}