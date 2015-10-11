namespace MetaMind.Engine.Entities.Graphics
{
    public interface IMMRenderOpacityOperations
    {
        
    }

    internal interface IMMRenderOpacityOperationsInternal
    {
        void DisableCascade();

        void UpdateCascade(IMMRenderOpacity parent);

        void UpdateDisplayed(IMMRenderOpacity parent);
    }
}
