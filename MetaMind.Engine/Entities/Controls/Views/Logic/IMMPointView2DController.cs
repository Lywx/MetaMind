namespace MetaMind.Engine.Entities.Controls.Views.Logic
{
    using Layouts;
    using Scrolls;
    using Selections;

    public interface IMMPointView2DController : IMMPointViewVerticalController, IMMPointViewHorizontalController 
    {
        new IMMPointView2DLayout ViewLayout { get; }

        new IMMPointView2DSelectionController ViewSelection { get; }

        new IMMPointView2DScrollController ViewScroll { get; }
    }
}