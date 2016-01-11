namespace MetaMind.Engine.Entities.Nodes
{
    using Bases;
    using Graphics;

    public interface IMMNodeBase : 
        IMMInputEntity, 
        ICCUpdatable, 
        IMMFocusable,
        IMMNodeOrganization
    {
    }

    public interface IMMNodeOperations : 
        IMMNodeScheduleOperations
    {
    }

    public interface IMMNode : 
        IMMNodeBase,
        IMMNodeOperations
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