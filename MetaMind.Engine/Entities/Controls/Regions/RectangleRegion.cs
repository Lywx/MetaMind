namespace MetaMind.Engine.Entities.Controls.Regions
{
    using Entities.Elements;
    using Entities.Elements.Rectangles;

    public class RectangleRegion : RegionElement, IRegion
    {
        public RectangleRegion()
        {
            this.InitializeElement();
            this.InitializeStates();
            this.RegisterHandlers();
        }

        #region Dependency

        public MMDraggableRectangleElement ImmRectangle { get; protected set; }

        #endregion

        #region Initialization

        private void InitializeStates()
        {
            this[RegionState.Mouse_Is_Over] = () => this.ImmRectangle[MMElementState.Mouse_Is_Over]();

            this[RegionState.Region_Has_Focus]  = () => this.RegionMachine.IsInState(RegionMachienState.HasFocus);
            this[RegionState.Region_Lost_Focus] = () => this.RegionMachine.IsInState(RegionMachienState.LostFocus);
        }

        private void InitializeElement()
        {
            this.ImmRectangle = new MMDraggableRectangleElement();
        }

        #endregion

        #region Events

        private void RectangleMousePressLeft(object sender, MMElementEventArgs e)
        {
            this.RegionMachine.Fire(RegionMachineTrigger.FocusInside);
        }

        private void RectangleMousePressOutLeft(object sender, MMElementEventArgs e)
        {
            this.RegionMachine.Fire(RegionMachineTrigger.FocusOutside);
        }

        #endregion

        #region Event Registration

        private void RegisterHandlers()
        {
            this.ImmRectangle.MousePressLeft    += this.RectangleMousePressLeft;
            this.ImmRectangle.MousePressOutLeft += this.RectangleMousePressOutLeft;
        }

        #endregion
    }
}