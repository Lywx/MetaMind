namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Regions;

    using Microsoft.Xna.Framework;

    public class ViewRegion : Region
    {
        public ViewRegion(Rectangle rectangle, Func<Point> getLocation)
            : base(rectangle)
        {
            this.GetLocation = getLocation;
        }

        #region Dependency

        protected Func<Point> GetLocation { get; set; }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            this.Location = this.GetLocation();
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