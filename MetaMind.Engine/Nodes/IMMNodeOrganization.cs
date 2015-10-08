namespace MetaMind.Engine.Nodes
{
    public interface IMMNodeOrganization
    {
        IMMNode Parent { get; } 

        MMNodeCollection Children { get; }
    }

    public interface IMMNodeOrganizationInternal
    {
        IMMNode Parent { get; set; } 

        MMNodeCollection Children { get; set; }
    }
}