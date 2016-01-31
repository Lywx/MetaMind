namespace MetaMind.Engine.Core.Entity.Node.Model
{
    using Controller;
    using Entity.Common;
    using Graphics;
    using View;

    public interface __IMMNodeBaseOrganizational
    {
        IMMNode Parent { get; } 

        MMNodeCollection Children { get; }
    }

    public interface __IMMNodeBaseOrganizationalInternal : __IMMNodeBaseOrganizational
    {
        new IMMNode Parent { get; set; } 
    }

    public interface __IMMNodeBase : __IMMNodeBaseOrganizational, IMMInputtableEntity, ICCUpdatable, IMMFocusable
    {
    }

    public interface __IMMNodeOperations : IMMSchedulableOperations
    {
    }

    public interface IMMNode : __IMMNodeBase, __IMMNodeOperations
    {
        IMMRendererOpacity Opacity { get; }

        IMMNodeColor Color { get; }
    }
 
    public interface IMMNodeInternal : IMMNode, __IMMNodeBaseOrganizationalInternal 
    {
        IMMNodeRenderer Renderer { get; }

        IMMNodeController Controller { get; }
    }
}