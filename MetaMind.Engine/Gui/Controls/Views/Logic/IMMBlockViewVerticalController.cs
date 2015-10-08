namespace MetaMind.Engine.Gui.Controls.Views.Logic
{
    using Scrolls;
    using Selections;

    public interface IMMBlockViewVerticalController : IMMPointViewVerticalController
    {
        new IBlockViewVerticalScrollController ViewScroll { get; }

        new IBlockViewVerticalSelectionController ViewSelection { get; }
    }
}