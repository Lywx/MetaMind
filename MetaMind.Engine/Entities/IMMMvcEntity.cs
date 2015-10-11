namespace MetaMind.Engine.Entities
{
    public interface IMMMVCEntity : IMMInputEntity
    {
        IMMMVCEntityController Controller { get; }

        IMMMVCEntityRenderer Renderer { get; }
    }
}