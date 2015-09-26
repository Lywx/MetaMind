namespace MetaMind.Engine.Gui.Controls.Item.Logic
{
    using Interactions;
    using Layouts;

    public interface IIndexBlockViewVerticalItemLogic : IIndexBlockViewVerticalItemLogicOperations, IBlockViewVerticalItemLogic
    {
        new IIndexBlockViewVerticalItemLayout ItemLayout { get; }

        new IIndexBlockViewVerticalItemInteraction ItemInteraction { get; }
    }
}