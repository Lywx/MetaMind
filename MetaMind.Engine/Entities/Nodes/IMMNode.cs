namespace MetaMind.Engine.Entities.Nodes
{
    using Graphics;

    public interface IMMNode : IMMNodeOrganization, IMMInputEntity, ICCUpdatable, IMMFocusable
    {
        IMMRendererOpacity Opacity { get; }

        IMMNodeColor Color { get; }
    }
 
    public interface IMMNodeInternal : IMMNodeOrganizationInternal 
    {
        IMMNodeRenderer Renderer { get; }

        IMMNodeController Controller { get; }
    }
}