namespace MetaMind.Engine.Entities.Controls.Item.Controllers
{
    using Layouts;

    public interface IMMBlockViewVerticalItemController : IMMPointViewVerticalItemController
    {
        new IMMBlockViewVerticalItemLayout ItemLayout { get; }
    }
}