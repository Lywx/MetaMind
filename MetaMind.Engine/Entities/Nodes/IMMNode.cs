namespace MetaMind.Engine.Entities.Nodes
{
    using Graphics;

    public interface IMMNodeBase : IMMInputEntity, ICCUpdatable, IMMFocusable
    {
        
    }

    public interface IMMNode : IMMNodeBase,
        IMMNodeOrganization,
        IMMNodeScheduleOperations
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