namespace MetaMind.Engine.Guis.Widgets.Items.Frames
{
    using MetaMind.Engine.Guis.Elements;

    public interface IItemRootFrame : IDraggableFrame
    {
        bool IsActive { get; set; }
    }
}