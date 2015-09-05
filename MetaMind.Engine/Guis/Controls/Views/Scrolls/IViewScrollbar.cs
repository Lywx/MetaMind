namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    using Elements;

    public interface IViewScrollbar : IDraggableFrame, IGameControllableEntity 
    {
        void Toggle();
    }
}