namespace MetaMind.Engine.Core.Entity.Control.Views.Controllers
{
    using Scrolls;
    using Selections;

    public interface IMMBlockViewVerticalController : IMMPointViewVerticalController
    {
        new IMMBlockViewVerticalScrollController ViewScroll { get; }

        new IMMBlockViewVerticalSelectionController ViewSelection { get; }
    }
}