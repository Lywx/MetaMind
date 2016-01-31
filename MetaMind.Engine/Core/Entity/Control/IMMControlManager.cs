namespace MetaMind.Engine.Core.Entity.Control
{
    using Entity.Common;

    public interface IMMControlManager : IMMControlManagerOperations, IMMReactor, IMMEntity
    {
        IMMControlComponent FocusedComponent { get; }

        MMControlCollection Components { get; }
    }

    public interface IMMControlManagerInternal : IMMControlManagerOperations, IMMReactor, IMMEntity
    {
        IMMControlComponent FocusedComponent { get; set; }

        MMControlCollection Components { get; }
    }
}