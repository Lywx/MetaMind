namespace MetaMind.Engine.Entities.Controls.Item.Logic
{
    using Layouts;

    public interface IMMBlockViewVerticalItemController : IMMPointViewVerticalItemController
    {
        new IMMBlockViewVerticalItemLayout ItemLayout { get; }
    }
}