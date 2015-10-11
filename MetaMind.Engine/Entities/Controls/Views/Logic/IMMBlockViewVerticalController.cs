namespace MetaMind.Engine.Entities.Controls.Views.Logic
{
    using Scrolls;
    using Selections;

    public interface IMMBlockViewVerticalController : IMMPointViewVerticalController
    {
        new IMMBlockViewVerticalScrollController ViewScroll { get; }

        new IMMBlockViewVerticalSelectionController ViewSelection { get; }
    }
}