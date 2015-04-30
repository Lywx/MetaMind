namespace MetaMind.Engine.Guis.Widgets.Views.PointView.ViewItem
{
    using MetaMind.Engine.Guis.Widgets.Items;

    public interface IPointView2DItemLogic : IViewItemLogic
    {
        int Column { get; set; }

        int Row { get; set; }
    }
}