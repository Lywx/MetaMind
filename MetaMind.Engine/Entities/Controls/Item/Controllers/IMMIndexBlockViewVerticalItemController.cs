namespace MetaMind.Engine.Entities.Controls.Item.Controllers
{
    using Interactions;
    using Layouts;

    public interface IMMIndexBlockViewVerticalItemController : IMMIndexBlockViewVerticalItemControllerOperations, IMMBlockViewVerticalItemController
    {
        new IMMIndexBlockViewVerticalItemLayout ItemLayout { get; }

        new IMMIndexBlockViewVerticalItemInteraction ItemInteraction { get; }
    }
}