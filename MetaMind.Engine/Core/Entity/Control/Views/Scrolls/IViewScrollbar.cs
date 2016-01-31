namespace MetaMind.Engine.Core.Entity.Control.Views.Scrolls
{
    using Entity.Common;
    using Entity.Input;

    public interface IViewScrollbar : IMMDraggableRectangleElement, IMMInputtableEntity 
    {
        void Toggle();
    }
}