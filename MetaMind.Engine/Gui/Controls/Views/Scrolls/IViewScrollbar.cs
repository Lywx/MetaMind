namespace MetaMind.Engine.Gui.Controls.Views.Scrolls
{
    using Elements.Rectangles;
    using Entities;

    public interface IViewScrollbar : IDraggableRectangle, IMMInputableEntity 
    {
        void Toggle();
    }
}