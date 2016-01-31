namespace MetaMind.Engine.Core.Entity.Control.Item.Controllers
{
    using Layouts;

    public interface IMMPointViewItemController : IMMViewItemController
    {
        new IMMPointViewItemLayout ItemLayout { get; }
    }
}