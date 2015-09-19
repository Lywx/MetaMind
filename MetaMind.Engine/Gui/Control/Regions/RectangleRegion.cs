namespace MetaMind.Engine.Gui.Control.Regions
{
    using Element;
    using Element.Rectangles;

    public class RectangleRegion : RegionElement, IRegion
    {
        public RectangleRegion()
        {
            this.InitializeElement();
            this.InitializeStates();
            this.RegisterHandlers();
        }

        #region Dependency

        public DraggableRectangle Rectangle { get; protected set; }

        #endregion

        #region Initialization

        private void InitializeStates()
        {
            this[RegionState.Mouse_Is_Over] = () => this.Rectangle[ElementState.Mouse_Is_Over]();

            this[RegionState.Region_Has_Focus]  = () => this.RegionMachine.IsInState(RegionMachienState.HasFocus);
            this[RegionState.Region_Lost_Focus] = () => this.RegionMachine.IsInState(RegionMachienState.LostFocus);
        }

        private void InitializeElement()
        {
            this.Rectangle = new DraggableRectangle();
        }

        #endregion

        #region Events

        private void RectangleMousePressLeft(object sender, ElementEventArgs e)
        {
            this.RegionMachine.Fire(RegionMachineTrigger.FocusInside);
        }

        private void RectangleMousePressOutLeft(object sender, ElementEventArgs e)
        {
            this.RegionMachine.Fire(RegionMachineTrigger.FocusOutside);
        }

        #endregion

        #region Event Registration

        private void RegisterHandlers()
        {
            this.Rectangle.MousePressLeft    += this.RectangleMousePressLeft;
            this.Rectangle.MousePressOutLeft += this.RectangleMousePressOutLeft;
        }

        #endregion
    }
}