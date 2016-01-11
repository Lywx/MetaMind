namespace MetaMind.Engine.Entities.Controls.Views.Scrolls
{
    using Bases;
    using Entities;
    using Entities.Elements.Rectangles;

    public interface IViewScrollbar : IMMDraggableRectangleElement, IMMInputEntity 
    {
        void Toggle();
    }
}