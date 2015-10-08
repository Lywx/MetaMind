namespace MetaMind.Engine.Gui.Controls
{
    public interface IMMControlComponentOrganizationInternal
    {
        MMControlCollection Children { get; set; }

        IMMControlComponentInternal Parent { get; set; }

        IMMControlComponentInternal Root { get; set;}
    }

    public interface IMMControlComponentOrganization
    {
        MMControlCollection Children { get; }

        IMMControlComponent Parent { get; }

        IMMControlComponent Root { get; }

        bool IsChild { get; }

        bool IsParent { get; }

        bool IsRoot { get; }
    }
}
