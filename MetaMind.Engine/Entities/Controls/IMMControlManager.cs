namespace MetaMind.Engine.Entities.Controls
{
    using Bases;
    using Entities;

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