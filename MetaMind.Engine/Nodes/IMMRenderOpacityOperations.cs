namespace MetaMind.Engine.Nodes
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
