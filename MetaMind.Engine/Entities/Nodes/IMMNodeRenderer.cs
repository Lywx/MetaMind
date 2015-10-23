namespace MetaMind.Engine.Entities.Nodes
{
    using Entities.Graphics;

    public interface IMMNodeRenderer : IMMRendererComponent
    {
        IMMNodeColor Color { get; set; }
    }
}