namespace MetaMind.Engine.Core.Entity.Control.Item.Layouts
{
    using Data;

    public interface IMMBlockViewVerticalItemLayout : IMMPointViewItemLayout
    {
        int BlockRow { get; }

        IMMBlockViewItemData BlockData { get; }

        /// <summary>
        /// Wrapped block text content
        /// </summary>
        string BlockStringWrapped { get; }
    }
}