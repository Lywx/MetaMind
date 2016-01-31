namespace MetaMind.Engine.Core.Entity.Control.Item.Controllers
{
    using Layouts;

    public interface IMMBlockViewVerticalItemController : IMMPointViewVerticalItemController
    {
        new IMMBlockViewVerticalItemLayout ItemLayout { get; }
    }
}