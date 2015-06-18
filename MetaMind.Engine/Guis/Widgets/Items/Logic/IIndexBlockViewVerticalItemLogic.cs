namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    using Layouts;
    using Views;

    public interface IIndexBlockViewVerticalItemLogic : IIndexBlockViewVerticalItemLogicOperations, IBlockViewVerticalItemLogic
    {
        new IIndexBlockViewVerticalItemLayout ItemLayout { get; }

        #region Index

        bool IndexedViewOpened { get; }

        IView IndexedView { get; }

        #endregion
    }
}