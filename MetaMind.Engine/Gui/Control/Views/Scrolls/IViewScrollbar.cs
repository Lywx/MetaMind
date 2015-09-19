namespace MetaMind.Engine.Gui.Control.Views.Scrolls
{
    using Element.Rectangles;

    public interface IViewScrollbar : IDraggableRectangle, IGameControllableEntity 
    {
        void Toggle();
    }
}