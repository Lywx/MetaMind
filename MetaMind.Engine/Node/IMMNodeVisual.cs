namespace MetaMind.Engine.Node
{
    using Gui.Renders;

    public interface IMMNodeVisual : IMMRenderComponent
    {
        IMMNodeColor Color { get; set; }
    }
}