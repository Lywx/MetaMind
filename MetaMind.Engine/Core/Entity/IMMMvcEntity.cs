namespace MetaMind.Engine.Core.Entity
{
    using Entity.Common;

    public interface IMMMVCEntity : IMMInputtableEntity
    {
        IMMMVCEntityController Controller { get; }

        IMMMVCEntityRenderer Renderer { get; }
    }
}