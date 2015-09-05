namespace MetaMind.Engine.Guis.Widgets.Regions
{
    using System;

    public interface IRegionEntity : IGameControllableEntity
    {
        bool[] States { get; }

        Func<bool> this[RegionState state] { get; set; }
    }
}