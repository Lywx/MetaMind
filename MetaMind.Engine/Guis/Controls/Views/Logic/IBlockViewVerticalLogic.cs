namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using Scrolls;
    using Selections;

    public interface IBlockViewVerticalLogic : IPointViewVerticalLogic
    {
        new IBlockViewVerticalScrollController ViewScroll { get; }

        new IBlockViewVerticalSelectionController ViewSelection { get; }
    }
}