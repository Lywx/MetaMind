namespace MetaMind.Engine.Gui.Controls.Views.Settings
{
    public interface IPointViewVerticalSettings : IPointViewSettings
    {
        int ViewRowDisplay { get; set; }

        int ViewRowMax { get; set; }
    }
}