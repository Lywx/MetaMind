namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    public interface IPointView2DItemLogic : IViewItemLogic
    {
        int Column { get; set; }

        int Row { get; set; }
    }
}