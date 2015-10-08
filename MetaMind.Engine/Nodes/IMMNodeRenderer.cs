namespace MetaMind.Engine.Nodes
{
    using Gui.Renders;

    public interface IMMNodeRenderer : IMMRenderComponent
    {
        IMMNodeColor Color { get; set; }
    }
}