namespace MetaMind.Engine.Gui.Controls
{
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