namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    using Interactions;
    using Layouts;

    public interface IIndexBlockViewVerticalItemLogic : IIndexBlockViewVerticalItemLogicOperations, IBlockViewVerticalItemLogic
    {
        new IIndexBlockViewVerticalItemLayout ItemLayout { get; }

        new IIndexBlockViewVerticalItemInteraction ItemInteraction { get; }
    }
}