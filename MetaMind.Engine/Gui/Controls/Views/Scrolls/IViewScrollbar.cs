namespace MetaMind.Engine.Gui.Controls.Views.Scrolls
{
    using Elements.Rectangles;

    public interface IViewScrollbar : IDraggableRectangle, IGameInputableEntity 
    {
        void Toggle();
    }
}