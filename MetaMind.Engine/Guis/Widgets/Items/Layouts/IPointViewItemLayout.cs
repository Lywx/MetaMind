namespace MetaMind.Engine.Guis.Widgets.Items.Layouts
{
    /// <summary>
    /// Item layout for all point view item including point view 1d or 2d
    /// </summary>
    public interface IPointViewItemLayout : IViewItemLayout
    {
        int Row { get; set; }

        int Column { get; set; }
    }
}