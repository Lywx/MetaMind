namespace MetaMind.Engine.Core.Entity.Graphics
{
    public interface IMMRendererOpacity
    {
        byte Raw { get; set; }

        byte Blend { get; }

        bool CascadeEnabled { get; set; }
    }
}