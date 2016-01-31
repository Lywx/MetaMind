namespace MetaMind.Engine.Core.Entity.Control.Views.Settings
{
    public interface IPointViewVerticalSettings : IPointViewSettings
    {
        int ViewRowDisplay { get; set; }

        int ViewRowMax { get; set; }
    }
}