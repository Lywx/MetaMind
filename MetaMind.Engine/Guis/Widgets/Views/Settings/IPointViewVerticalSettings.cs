namespace MetaMind.Engine.Guis.Widgets.Views.Settings
{
    public interface IPointViewVerticalSettings : IPointViewSettings
    {
        int ViewRowDisplay { get; set; }
        int ViewRowMax { get; set; }
    }
}