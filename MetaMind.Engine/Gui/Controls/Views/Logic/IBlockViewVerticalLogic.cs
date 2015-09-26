namespace MetaMind.Engine.Gui.Controls.Views.Logic
{
    using Scrolls;
    using Selections;

    public interface IBlockViewVerticalLogic : IPointViewVerticalLogic
    {
        new IBlockViewVerticalScrollController ViewScroll { get; }

        new IBlockViewVerticalSelectionController ViewSelection { get; }
    }
}