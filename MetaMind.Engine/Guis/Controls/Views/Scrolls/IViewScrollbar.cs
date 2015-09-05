namespace MetaMind.Engine.Guis.Controls.Views.Scrolls
{
    using Elements;

    public interface IViewScrollbar : IDraggableFrame, IGameControllableEntity 
    {
        void Toggle();
    }
}