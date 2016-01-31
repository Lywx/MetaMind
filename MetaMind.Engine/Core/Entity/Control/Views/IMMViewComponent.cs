namespace MetaMind.Engine.Core.Entity.Control.Views
{
    using Entity.Common;

    // TODO???
    public interface IS : IMMReactor, IMMInputtableEntity 
    {
        
    }

    public interface IMMViewComponent : IS, IMMViewComponentOperations 
    {
        IMMView View { get; }
    }
}