namespace MetaMind.Engine.Core.Entity.Node.Controller
{
    using Common;
    using Control;

    public interface __IMMNodeControllerBase : IMMControlComponent 
    {
        
    }

    public interface __IMMNodeControllerOperations : IMMSchedulableOperations
    {
        
    }

    public interface IMMNodeController : __IMMNodeControllerBase, __IMMNodeControllerOperations
    {
    }
}