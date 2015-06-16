namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    using Layouts;
    using Views;

    public interface IIndexBlockViewVerticalItemLogic : IIndexBlockViewVerticalItemLogicOperations, IBlockViewVerticalItemLogic
    {
        new IIndexBlockViewVerticalItemLayout ItemLayout { get; }

        #region Index

        bool IndexViewOpened { get; }

        IView IndexView { get; }

        #endregion
    }
}