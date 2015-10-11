namespace MetaMind.Engine.Entities.Controls.Item.Logic
{
    using Layouts;

    public interface IMMPointViewItemController : IMMViewItemController
    {
        new IMMPointViewItemLayout ItemLayout { get; }
    }
}