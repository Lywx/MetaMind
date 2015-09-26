namespace MetaMind.Engine.Gui.Components
{
    using Elements.Rectangles;

    public class Component : RectangleElement, IComponent
    {
        #region Initialization

        public bool Initialized { get; private set; }

        public virtual void Initialize()
        {
            this.Initialized = true;
        }

        #endregion
    }
}