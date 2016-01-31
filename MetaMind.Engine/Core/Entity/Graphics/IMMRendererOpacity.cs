namespace MetaMind.Engine.Core.Entity.Graphics
{
    public interface __IMMRendererOpacityOperations
    {
        
    }

    internal interface __IMMRendererOpacityOperationsInternal : __IMMRendererOpacityOperations
    {
        void DisableCascade();

        void UpdateCascade(IMMRendererOpacity parent);

        void UpdateDisplayed(IMMRendererOpacity parent);
    }

    public interface IMMRendererOpacity : __IMMRendererOpacityOperations 
    {
        byte Raw { get; set; }

        byte Blend { get; }

        bool CascadeEnabled { get; set; }
    }

    internal interface IMMRendererOpacityInternal : IMMRendererOpacity, __IMMRendererOpacityOperationsInternal
    {
        IMMRendererComponent Target { get; }

        new byte Blend { set; get; }
    }
}