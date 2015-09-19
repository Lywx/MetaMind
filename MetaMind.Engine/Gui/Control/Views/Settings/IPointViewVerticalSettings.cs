namespace MetaMind.Engine.Gui.Control.Views.Settings
{
    public interface IPointViewVerticalSettings : IPointViewSettings
    {
        int ViewRowDisplay { get; set; }
        int ViewRowMax { get; set; }
    }
}