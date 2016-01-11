namespace MetaMind.Engine.Entities
{
    using Bases;

    public interface IMMMVCEntity : IMMInputEntity
    {
        IMMMVCEntityController Controller { get; }

        IMMMVCEntityRenderer Renderer { get; }
    }
}