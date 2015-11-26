namespace MetaMind.Engine.Entities.Controls.Item.Controllers
{
    using Layouts;

    public interface IMMPointViewItemController : IMMViewItemController
    {
        new IMMPointViewItemLayout ItemLayout { get; }
    }
}