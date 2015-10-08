namespace MetaMind.Engine.Gui.Controls.Views.Logic
{
    using Layouts;
    using Scrolls;
    using Selections;

    public interface IMMPointView2DController : IMMPointViewVerticalController, IMMPointViewHorizontalController 
    {
        new IPointView2DLayout ViewLayout { get; }

        new IPointView2DSelectionController ViewSelection { get; }

        new IPointView2DScrollController ViewScroll { get; }
    }
}