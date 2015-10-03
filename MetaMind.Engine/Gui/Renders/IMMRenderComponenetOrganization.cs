namespace MetaMind.Engine.Gui.Renders
{
    public interface IMMRenderComponenetOrganizationInternal
    {
        IMMRenderComponent Parent { get; set; }

        IMMRenderComponent Root { get; set; }
    }

    public interface IMMRenderComponenetOrganization
    {
        IMMRenderComponent Parent { get; }

        IMMRenderComponent Root { get; }

        MMRenderComponenetCollection Children { get; }

        bool IsChild { get; }

        bool IsParent { get; }
    }
}
