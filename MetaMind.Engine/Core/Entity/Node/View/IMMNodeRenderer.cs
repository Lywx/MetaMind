namespace MetaMind.Engine.Core.Entity.Node.View
{
    using Entity.Graphics;
    using Entity.Node.Model;

    public interface IMMNodeRenderer : IMMRendererComponent
    {
        IMMNodeColor Color { get; set; }
    }
}