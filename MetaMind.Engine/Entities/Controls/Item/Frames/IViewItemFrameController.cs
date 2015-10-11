namespace MetaMind.Engine.Entities.Controls.Item.Frames
{
    /// <summary>
    /// A interface that applies to all frame controller.
    /// </summary>
    ///<remarks>
    /// Most of the derived classes are used as generics in view/item through 
    /// layering. It doesn't really need to be setup real member here.
    /// </remarks>
    public interface IMMViewItemFrameController : IMMViewItemController
    {
    }
}