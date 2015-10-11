namespace MetaMind.Engine.Entities.Controls.Item.Logic
{
    using Interactions;
    using Layouts;

    public interface IMMIndexBlockViewVerticalItemController : IIndexBlockViewVerticalItemLogicOperations, IMMBlockViewVerticalItemController
    {
        new IMMIndexBlockViewVerticalItemLayout ItemLayout { get; }

        new IMMIndexBlockViewVerticalItemInteraction ItemInteraction { get; }
    }
}