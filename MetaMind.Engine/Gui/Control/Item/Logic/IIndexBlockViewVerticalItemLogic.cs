namespace MetaMind.Engine.Gui.Control.Item.Logic
{
    using Interactions;
    using Layouts;

    public interface IIndexBlockViewVerticalItemLogic : IIndexBlockViewVerticalItemLogicOperations, IBlockViewVerticalItemLogic
    {
        new IIndexBlockViewVerticalItemLayout ItemLayout { get; }

        new IIndexBlockViewVerticalItemInteraction ItemInteraction { get; }
    }
}