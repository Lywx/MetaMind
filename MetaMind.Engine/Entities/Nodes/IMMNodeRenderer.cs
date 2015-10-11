namespace MetaMind.Engine.Entities.Nodes
{
    using Entities.Graphics;

    public interface IMMNodeRenderer : IMMRenderComponent
    {
        IMMNodeColor Color { get; set; }
    }
}