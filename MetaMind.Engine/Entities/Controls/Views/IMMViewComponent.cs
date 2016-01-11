namespace MetaMind.Engine.Entities.Controls.Views
{
    using Bases;
    using Entities;

    // TODO???
    public interface IS : IMMReactor, IMMInputEntity 
    {
        
    }

    public interface IMMViewComponent : IS, IMMViewComponentOperations 
    {
        IMMView View { get; }
    }
}